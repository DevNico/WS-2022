environment = "prod"

keycloak_users = {
  "mail@tschmitt.eu" = {
    first_name = "Thorsten",
    last_name  = "Hero"
  },
  "martin@mseiten.de" = {
    first_name = "Martin",
    last_name  = "Seiten"
  },
  "nicolasschlecker@gmail.com" = {
    first_name = "Nicolas",
    last_name  = "Schlecker"
  },
  "ws22@markusjx.com" = {
    first_name = "Markus",
    last_name  = "Jaeger"
  }
  "philipp.cerweny@gmx.de" = {
    first_name = "Philipp",
    last_name  = "Cerweny"
  }
}

webapp_redirect_urls = [
  "https://localhost:*",
  "https://localhost:*/*",
  "https://srm.devnico.cloud",
  "https://srm.devnico.cloud/*",
  "https://api.srm.devnico.cloud/swagger/oauth2-redirect.html"
]
backend_redirect_urls = ["*"]
