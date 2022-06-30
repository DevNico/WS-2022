module "env" {
  count  = var.deploy_env ? 1 : 0
  source = "./modules/env"

  project_id  = var.project_id
  environment = var.environment
  namespace   = local.namespace

  keycloak_users        = var.keycloak_users
  webapp_redirect_urls  = var.webapp_redirect_urls
  backend_redirect_urls = var.backend_redirect_urls

  github_token = var.github_token
}
