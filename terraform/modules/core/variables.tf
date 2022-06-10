variable "project_id" {
  description = "The Google Cloud project ID"
  type        = string
}

variable "github_repository" {
  description = "The Github User/Repository"
  type        = string
}

variable "namespace" {
  description = "The Kubernetes namespace to use"
  type        = string
}

variable "keycloak_url" {
  description = "The keycloak url"
  type        = string
}

variable "keycloak_username" {
  description = "The keycloak admin username"
  type        = string
}

variable "keycloak_password" {
  description = "The keycloak admin password"
  type        = string
}
