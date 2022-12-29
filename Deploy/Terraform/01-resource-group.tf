resource "azurerm_resource_group" "markup-processor-rg" {
  location = var.resource_group_location
  name     = var.resource_group_name
}