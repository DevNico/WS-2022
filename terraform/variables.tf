variable "project_id" {
  description = "The Google Cloud project ID"
  type        = string
}

variable "deploy_core" {
  description = "Whether to deploy the core"
  type        = bool
  default     = false
}
variable "deploy_env" {
  description = "Whether to deploy the environment"
  type        = bool
  default     = false
}

variable "domain" {
  description = "The Cluster domain"
  type        = string
}

variable "github_repository" {
  description = "The Github User/Repository"
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

variable "github_token" {
  description = "The github token to access repositories"
  type        = string
}
