variable "project_id" {
  description = "The Google Cloud project id."
  default     = "service-release-manager"
}

variable "github_repository" {
  description = "The Github User/Repository"
  default     = "DevNico/WS-2022"
}

variable "environment" {
  description = "The environment to deploy to."
  default     = "dev"
}

variable "keycloak_url" {
  description = "The keycloak url"
  default     = "idp.srm.k3s.devnico.cloud"
}

variable "keycloak_username" {
  description = "The keycloak admin username"
}

variable "keycloak_password" {
  description = "The keycloak admin password"
}
