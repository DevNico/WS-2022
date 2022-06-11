resource "kubernetes_namespace" "namespace" {
  metadata {
    name = var.namespace
  }
}

module "keycloak" {
  source = "github.com/DevNico/terraform-k3s-keycloak?ref=v0.0.1"

  name      = "keycloak"
  namespace = resource.kubernetes_namespace.namespace.metadata[0].name

  url            = var.keycloak_url
  admin_username = var.keycloak_username
  admin_password = var.keycloak_password
}
