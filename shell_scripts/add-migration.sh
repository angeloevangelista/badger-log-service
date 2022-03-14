#!/bin/bash

RED='\033[0;31m'
NO_COLOR='\033[0m' # No Color

MIGRATION_NAME=$1

if [ -z "$MIGRATION_NAME" ]
then
  printf "${RED}Invalid usage.${NO_COLOR}\n"
  echo "Usage: add-migration <migration name>"
else
  dotnet ef migrations add ${MIGRATION_NAME} --project ./BadgerLogService.Data/ --startup-project ./BadgerLogService.WebApi
fi
