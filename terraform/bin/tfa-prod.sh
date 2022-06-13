#!/bin/zsh
alias tf="terraform"

tf workspace select prod
tf apply -var-file=env/core.tfvars -var-file=env/prod.tfvars -var="deploy_env=true"
