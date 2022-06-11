variable "environment" {
  description = "The environment to deploy to"
  type        = string
}

variable "project_id" {
  description = "The project ID to deploy to"
  type        = string
}

variable "namespace" {
  description = "The Kubernetes namespace to use"
  type        = string
}

variable "webapp_redirect_urls" {
  description = "The redirect urls for the webapp client"
  type        = list(string)
}

variable "backend_redirect_urls" {
  description = "The redirect urls for the backend client"
  type        = list(string)
}

variable "keycloak_users" {
  description = "The keycloak users to create"
  type = map(object({
    first_name = string
    last_name  = string
  }))
}
