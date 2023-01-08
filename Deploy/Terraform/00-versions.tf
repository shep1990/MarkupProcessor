terraform {
  required_providers {
    azuread = {
      source  = "hashicorp/azuread"
      version = "1.5.0"
    }

    azurerm = {
      source = "hashicorp/azurerm"
      version = "3.38"
    }
  }

  backend "azurerm" {}
}

provider "azurerm" {
  features {}
  skip_provider_registration = true
}
