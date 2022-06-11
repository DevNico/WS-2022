#!/bin/zsh
alias tf="terraform"

tf workspace select prod
tf destroy -var-file=env/core.tfvars -var-file=env/prod.tfvars

tf workspace select dev
tf destroy -var-file=env/core.tfvars -var-file=env/dev.tfvars

tf workspace select core
tf destroy -var-file=env/core.tfvars
