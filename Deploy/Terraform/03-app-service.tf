resource "azurerm_service_plan" "personal_projects" {
  name                = "personal_app_service_plan"
  resource_group_name = data.azurerm_resource_group.markup-processor-rg.name
  location            = data.azurerm_resource_group.markup-processor-rg.location
  os_type             = "Linux"
  sku_name            = "F1"
}

resource "azurerm_linux_web_app" "markup_app" {
  name                = "markup-processor-app"
  resource_group_name = data.azurerm_resource_group.markup-processor-rg.name
  location            = azurerm_service_plan.personal_projects.location
  service_plan_id     = azurerm_service_plan.personal_projects.id

  site_config {
	always_on = false
  }
}