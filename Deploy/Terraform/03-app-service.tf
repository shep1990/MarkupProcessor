resource "azurerm_service_plan" "personal_projects" {
  name                = "personal_app_service_plan"
  resource_group_name = azurerm_resource_group.markup-processor-rg.name
  location            = azurerm_resource_group.markup-processor-rg.location
  os_type             = "Linux"
  sku_name            = "F1"
}

resource "azurerm_linux_web_app" "markup_app" {
  name                = "markup-processor-app"
  resource_group_name = azurerm_resource_group.markup-processor-rg.name
  location            = azurerm_service_plan.personal_projects.location
  service_plan_id     = azurerm_service_plan.personal_projects.id

  site_config {
	always_on = false
  }
}

resource "azurerm_app_service_source_control" "source_control" {
  app_id   = azurerm_linux_web_app.markup_app.id
  repo_url = "https://github.com/shep1990/MarkupProcessor"
  branch   = "master"
  use_manual_integration = true
}