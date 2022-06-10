resource "google_service_account" "gh_actions_deployment" {
  project      = var.project_id
  account_id   = "gh-actions-deployment"
  display_name = "Github Actions Deployment"
  description  = "Service Account for deployments via Github Actions"
  disabled     = false
}

resource "google_project_iam_member" "firebase_hosting_binding" {
  project = var.project_id
  role    = "roles/firebasehosting.admin"
  member  = "serviceAccount:${google_service_account.gh_actions_deployment.email}"
}

module "gh_oidc" {
  source      = "terraform-google-modules/github-actions-runners/google//modules/gh-oidc"
  project_id  = var.project_id
  pool_id     = "github-pool"
  provider_id = "actions-provider"
  sa_mapping = {
    "gh-actions-service-account" = {
      sa_name   = google_service_account.gh_actions_deployment.id
      attribute = "attribute.repository/${var.github_repository}"
    }
  }
}
