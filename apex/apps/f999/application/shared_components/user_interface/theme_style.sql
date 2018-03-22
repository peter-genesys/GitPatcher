prompt --application/shared_components/user_interface/theme_style
begin
wwv_flow_api.create_theme_style(
 p_id=>wwv_flow_api.id(11576267317509973)
,p_theme_id=>42
,p_name=>'Vita Purple'
,p_is_current=>true
,p_is_public=>false
,p_is_accessible=>false
,p_theme_roller_input_file_urls=>'#THEME_IMAGES#less/theme/Vita.less'
,p_theme_roller_config=>'{"customCSS":"","vars":{"@utr_Palette1-lighter":"#c17cff","@utr_Palette1-light":"#875faa","@utr_Palette1":"#6f4e8c","@g_Accent-OG":"#f6f1fa","@utr_Palette1-darker":"#4a345e","@g_Accent-BG":"#c17cff","@utr_Palette2-light":"#875faa","@utr_Palette3-norm'
||'al":"#6f4e8c","@utr_Palette3-dark":"#5b3f72","@utr_Palette2-darker":"#4a345e","@utr_Palette3-lighter":"#c17cff","@utr_Palette3-light":"#875faa","@utr_Palette3":"#6f4e8c","@utr_Palette3-darker":"#4a345e","@utr_Palette4-lighter":"#c17cff","@utr_Palette'
||'4-light":"#875faa","@utr_Palette4":"#6f4e8c","@utr_Palette4-dark":"#5b3f72","@utr_Palette4-darker":"#4a345e","@Side-Exp":"240px","@Nav-Exp":"200px"}}'
,p_theme_roller_output_file_url=>'#THEME_DB_IMAGES#11576267317509973.css'
,p_theme_roller_read_only=>false
);
wwv_flow_api.create_theme_style(
 p_id=>wwv_flow_api.id(90156070730216803)
,p_theme_id=>42
,p_name=>'Vista'
,p_css_file_urls=>'#THEME_IMAGES#css/Vista#MIN#.css?v=#APEX_VERSION#'
,p_is_current=>false
,p_is_public=>false
,p_is_accessible=>false
,p_theme_roller_read_only=>true
,p_reference_id=>4007676303523989775
);
wwv_flow_api.create_theme_style(
 p_id=>wwv_flow_api.id(90156241630216803)
,p_theme_id=>42
,p_name=>'Vita'
,p_is_current=>false
,p_is_public=>false
,p_is_accessible=>false
,p_theme_roller_input_file_urls=>'#THEME_IMAGES#less/theme/Vita.less'
,p_theme_roller_output_file_url=>'#THEME_IMAGES#css/Vita#MIN#.css?v=#APEX_VERSION#'
,p_theme_roller_read_only=>true
,p_reference_id=>2719875314571594493
);
wwv_flow_api.create_theme_style(
 p_id=>wwv_flow_api.id(90156438768216803)
,p_theme_id=>42
,p_name=>'Vita - Slate'
,p_is_current=>false
,p_is_public=>false
,p_is_accessible=>false
,p_theme_roller_input_file_urls=>'#THEME_IMAGES#less/theme/Vita-Slate.less'
,p_theme_roller_config=>'{"customCSS":"","vars":{"@g_Accent-BG":"#505f6d","@g_Accent-OG":"#ececec","@g_Body-Title-BG":"#dee1e4","@l_Link-Base":"#337ac0","@g_Body-BG":"#f5f5f5"}}'
,p_theme_roller_output_file_url=>'#THEME_IMAGES#css/Vita-Slate#MIN#.css?v=#APEX_VERSION#'
,p_theme_roller_read_only=>true
,p_reference_id=>3291983347983194966
);
end;
/
