--application/shared_components/user_interface/templates/button/small_button
prompt  ......Button Template 40763907402551119
 
begin
 
wwv_flow_api.create_button_templates (
  p_id => 40763907402551119 + wwv_flow_api.g_id_offset
 ,p_flow_id => wwv_flow.g_flow_id
 ,p_template_name => 'Small Button'
 ,p_template => 
'<a href="#LINK#" class="uButtonSmall" id="#BUTTON_ID#" #BUTTON_ATTRIBUTES# role="button"><span>#LABEL#</span></a> '
 ,p_hot_template => 
'<a href="#LINK#" class="uButtonSmall uHotButton" id="#BUTTON_ID#" #BUTTON_ATTRIBUTES# role="button"><span>#LABEL#</span></a> '
 ,p_translate_this_template => 'N'
 ,p_theme_class_id => 4
 ,p_theme_id => 23
  );
null;
 
end;
/

---------------------------------------
prompt  ...region templates
--
