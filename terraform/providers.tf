terraform {
  backend "gcs" {
    credentials = "GCP_TF_ADMIN.json"
    bucket      = "d6741a48-ec95-44c1-92b1-23a9d8520b5d"
    prefix      = "prod"
  }

  required_providers {
    github = {
      source  = "integrations/github"
      version = "4.24.1"
    }
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

provider "github" {
  owner = "DevNico"
  token = var.github_token
}

provider "google" {
  project = var.project_id

  credentials = file("GCP_TF_ADMIN.json")
  region      = "europe-west3"
}

provider "google-beta" {
  project = var.project_id

  credentials = file("GCP_TF_ADMIN.json")
  region      = "europe-west3"
}

provider "kubernetes" {
  config_path = "config.yaml"
}

provider "helm" {
  kubernetes {
    config_path = "config.yaml"
  }
}

provider "keycloak" {
  client_id     = "admin-cli"
  username      = var.keycloak_username
  password      = var.keycloak_password
  url           = "https://idp.${var.domain}"
  initial_login = false
  base_path     = ""
}
