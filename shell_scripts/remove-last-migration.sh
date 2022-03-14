#!/bin/bash

dotnet ef migrations remove --project ./BadgerLogService.Data/ --startup-project ./BadgerLogService.WebApi
