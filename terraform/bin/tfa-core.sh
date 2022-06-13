#!/bin/zsh
alias tf="terraform"

tf workspace select core
tf apply -var-file=env/core.tfvars -var="deploy_core=true"
