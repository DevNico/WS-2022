variable "environment" {
  description = "The environment to deploy to"
  type        = string
  default     = ""
}

variable "webapp_redirect_urls" {
  description = "The redirect urls for the webapp client"
  type        = list(string)
  default     = []
}

variable "backend_redirect_urls" {
  description = "The redirect urls for the backend client"
  type        = list(string)
  default     = []
}

variable "keycloak_users" {
  description = "The keycloak users to create"
  type = map(object({
    first_name = string
    last_name  = string
  }))
  default = {}
}
