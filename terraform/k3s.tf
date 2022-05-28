resource "kubernetes_namespace" "srm" {
  metadata {
    name = "service-release-manager"
  }
}

module "keycloak" {
  source = "github.com/DevNico/terraform-k3s-keycloak?ref=v0.0.1"

  name           = "keycloak"
  namespace      = resource.kubernetes_namespace.srm.metadata[0].name
  url            = var.keycloak_url
  admin_username = var.keycloak_username
  admin_password = var.keycloak_password
}
