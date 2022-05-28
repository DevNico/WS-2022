output "keycloak_admin_username" {
  value = module.keycloak.admin_username
}

output "keycloak_admin_password" {
  value     = module.keycloak.admin_password
  sensitive = true
}
