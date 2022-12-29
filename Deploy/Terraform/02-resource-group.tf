resource "azurerm_resource_group" "markup-processor-rg" {
  location = var.resource_group_location
  name     = data.azurerm_resource_group.markup-processor-rg.name
}