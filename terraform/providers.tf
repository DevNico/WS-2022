terraform {
  backend "gcs" {
    credentials = "GCP_TF_ADMIN.json"
    bucket      = "d6741a48-ec95-44c1-92b1-23a9d8520b5d"
    prefix      = "prod"
  }
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
