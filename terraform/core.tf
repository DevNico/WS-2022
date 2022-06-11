module "core" {
  count  = var.deploy_core ? 1 : 0
  source = "./modules/core"

  project_id        = var.project_id
  github_repository = var.github_repository

  namespace = local.namespace

  keycloak_url      = local.keycloak_url
  keycloak_username = var.keycloak_username
  keycloak_password = var.keycloak_password
}
