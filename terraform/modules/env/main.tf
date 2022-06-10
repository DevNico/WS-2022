terraform {
  required_providers {
    random = {
      source  = "hashicorp/random"
      version = "3.1.3"
    }
    null = {
      source  = "hashicorp/null"
      version = "3.1.1"
    }
    google = {
      source  = "hashicorp/google"
      version = "4.20.0"
    }
    helm = {
      source  = "hashicorp/helm"
      version = "2.5.1"
    }
    kubectl = {
      source  = "gavinbunney/kubectl"
      version = ">= 1.14.0"
    }
    keycloak = {
      source  = "mrparkers/keycloak"
      version = "3.7.0"
    }
  }
}
