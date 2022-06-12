data "google_secret_manager_secret" "sengrid_api_key" {
  project   = var.project_id
  secret_id = "sengrid_api_key"
}

data "google_secret_manager_secret_version" "sengrid_api_key" {
  project = var.project_id
  secret  = data.google_secret_manager_secret.sengrid_api_key.id
}

locals {
  backend_client_id = "backend-client"
  webapp_client_id  = "webapp-v1"
}

resource "keycloak_realm" "keycloak_env" {
  enabled           = true
  realm             = var.environment
  display_name      = var.environment
  display_name_html = format("<div class=\"kc-logo-text\"><span>%s</span></div>", var.environment)

  access_code_lifespan = "1h"

  ssl_required    = "external"
  password_policy = "upperCase(1) and length(8) and notUsername"

  registration_email_as_username = true
  login_with_email_allowed       = true
  duplicate_emails_allowed       = false
  reset_password_allowed         = true
  remember_me                    = true
  verify_email                   = true

  default_signature_algorithm = "RS256"

  internationalization {
    supported_locales = [
      "en",
      "de"
    ]
    default_locale = "de"
  }

  smtp_server {
    host = "smtp.sendgrid.net"
    from = "no-reply@srm.devnico.cloud"
    ssl  = true
    port = 465
    auth {
      username = "apikey"
      password = data.google_secret_manager_secret_version.sengrid_api_key.secret_data
    }
  }
}

resource "keycloak_role" "admin" {
  realm_id = keycloak_realm.keycloak_env.id
  name     = "admin"
}

resource "keycloak_role" "super_admin" {
  realm_id = keycloak_realm.keycloak_env.id
  name     = "superAdmin"
}

resource "keycloak_user" "user" {
  for_each       = var.keycloak_users
  realm_id       = keycloak_realm.keycloak_env.id
  enabled        = true
  email_verified = true

  email      = each.key
  username   = each.key
  first_name = each.value.first_name
  last_name  = each.value.last_name
}
resource "keycloak_user_roles" "user_roles" {
  for_each = keycloak_user.user
  realm_id = keycloak_realm.keycloak_env.id
  user_id  = each.value.id

  role_ids = [
    keycloak_role.super_admin.id
  ]
}

resource "keycloak_openid_client" "backend" {
  realm_id  = keycloak_realm.keycloak_env.id
  client_id = local.backend_client_id

  name    = "Backend Client"
  enabled = true

  access_type                  = "CONFIDENTIAL"
  service_accounts_enabled     = true
  standard_flow_enabled        = true
  direct_access_grants_enabled = true

  valid_redirect_uris = var.backend_redirect_urls

  login_theme = "keycloak"
}

resource "keycloak_openid_client" "webapp" {
  realm_id  = keycloak_realm.keycloak_env.id
  client_id = local.webapp_client_id

  name    = "WebApp Client"
  enabled = true

  access_type                  = "PUBLIC"
  standard_flow_enabled        = true
  implicit_flow_enabled        = true
  direct_access_grants_enabled = true

  valid_redirect_uris = var.webapp_redirect_urls
  web_origins         = ["*"]
}

resource "keycloak_openid_audience_protocol_mapper" "audience_mapper" {
  realm_id  = keycloak_realm.keycloak_env.id
  client_id = keycloak_openid_client.backend.id
  name      = "audience-mapper"

  included_client_audience = "security-admin-console"
}

resource "keycloak_openid_client_service_account_realm_role" "client_service_account_role" {
  realm_id                = keycloak_realm.keycloak_env.id
  service_account_user_id = keycloak_openid_client.backend.service_account_user_id
  role                    = keycloak_role.admin.name
}

data "keycloak_openid_client" "realm_management" {
  realm_id  = keycloak_realm.keycloak_env.id
  client_id = "realm-management"
}
resource "keycloak_openid_client_service_account_role" "realm_management_roles" {
  realm_id                = keycloak_realm.keycloak_env.id
  service_account_user_id = keycloak_openid_client.backend.service_account_user_id
  client_id               = data.keycloak_openid_client.realm_management.id
  role                    = "realm-admin"
}
