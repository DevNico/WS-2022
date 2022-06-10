data "kubernetes_namespace" "srm" {
  metadata {
    name = "service-release-manager"
  }
}

locals {
  namespace = data.kubernetes_namespace.srm.metadata[0].name
  db_name   = "srm-db-${var.environment}"
}

module "postgresql" {
  source  = "ballj/postgresql/kubernetes"
  version = "~> 1.0"

  namespace     = local.namespace
  name          = local.db_name
  object_prefix = local.db_name
}

data "kubernetes_secret" "postgres_password" {
  depends_on = [module.postgresql.name]
  metadata {
    name      = module.postgresql.password_secret
    namespace = local.namespace
  }
}

resource "kubernetes_config_map" "appsettings" {
  metadata {
    name      = "appsettings-${var.environment}"
    namespace = local.namespace
  }

  data = {
    "appsettings.Production.json" = <<JSON
{
  "ConnectionStrings": {
    "Default": "${
    join(";", tolist([
      "Host=${module.postgresql.hostname}",
      "Database=${module.postgresql.name}",
      "Username=${module.postgresql.username}",
      "Password=${data.kubernetes_secret.postgres_password.data[module.postgresql.password_key]}",
    ]))
  }"
  },
  "Swagger": {
    "Enabled": "true",
    "ClientId": "webapp-v1"
  },
  "Keycloak": {
    "Url": "https://idp.srm.k3s.devnico.cloud",
    "ClientId": "${local.backend_client_id}",
    "ClientSecret": "${keycloak_openid_client.backend.client_secret}",
    "Realm": "${keycloak_realm.keycloak_env.realm}",
    "AuthRealm": "${keycloak_realm.keycloak_env.realm}",
    "Audience": "account"
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "log.txt",
          "rollingInterval": "Day"
        }
      }
    ]
  }
}

  JSON
}
}
