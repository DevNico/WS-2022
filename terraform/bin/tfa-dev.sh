#!/bin/zsh
alias tf="terraform"

tf workspace select dev
tf apply -var-file=env/core.tfvars -var-file=env/dev.tfvars -var="deploy_env=true"
