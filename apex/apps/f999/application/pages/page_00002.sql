prompt --application/pages/page_00002
begin
wwv_flow_api.create_page(
 p_id=>2
,p_user_interface_id=>wwv_flow_api.id(72818849918899195)
,p_tab_set=>'TS1'
,p_name=>'Installed Patches'
,p_step_title=>'Installed Patches'
,p_reload_on_submit=>'A'
,p_warn_on_unsaved_changes=>'N'
,p_step_sub_title=>'Installed Patches'
,p_step_sub_title_type=>'TEXT_WITH_SUBSTITUTIONS'
,p_autocomplete_on_off=>'ON'
,p_page_template_options=>'#DEFAULT#'
,p_nav_list_template_options=>'#DEFAULT#'
,p_help_text=>'No help is available for this page.'
,p_last_updated_by=>'PETER'
,p_last_upd_yyyymmddhh24miss=>'20180322130451'
);
wwv_flow_api.create_page_plug(
 p_id=>wwv_flow_api.id(72821263151899214)
,p_plug_name=>'Installed Patches'
,p_region_template_options=>'#DEFAULT#'
,p_component_template_options=>'#DEFAULT#'
,p_plug_template=>wwv_flow_api.id(90111842105216762)
,p_plug_display_sequence=>10
,p_plug_display_point=>'BODY_3'
,p_query_type=>'SQL'
,p_plug_source=>wwv_flow_string.join(wwv_flow_t_varchar2(
'select ROWID,',
'    PATCHES_V.PATCH_NAME as PATCH_NAME,',
'    PATCHES_V.DB_SCHEMA as DB_SCHEMA,',
'    PATCHES_V.BRANCH_NAME as BRANCH_NAME,',
'    PATCHES_V.TAG_FROM as TAG_FROM,',
'    PATCHES_V.TAG_TO as TAG_TO,',
'    PATCHES_V.SUPPLEMENTARY as SUPPLEMENTARY,',
'    PATCHES_V.PATCH_DESC as PATCH_DESC,',
'    REPLACE(PATCHES_V.PATCH_COMPONANTS,'','',''<BR>'') as PATCH_COMPONANTS,',
'    PATCHES_V.PATCH_CREATE_DATE as PATCH_CREATE_DATE,',
'    PATCHES_V.PATCH_CREATED_BY as PATCH_CREATED_BY,',
'    PATCHES_V.NOTE as NOTE,',
'    PATCHES_V.LOG_DATETIME as LOG_DATETIME,',
'    PATCHES_V.COMPLETED_DATETIME as COMPLETED_DATETIME,',
'    PATCHES_V.SUCCESS_YN as SUCCESS_YN,',
'    PATCHES_V.RETIRED_YN as RETIRED_YN,',
'    PATCHES_V.WARNING_COUNT as WARNING_COUNT,',
'    PATCHES_V.RERUNNABLE_YN as RERUNNABLE_YN,',
'    PATCHES_V.ERROR_COUNT as ERROR_COUNT,',
'    PATCHES_V.USERNAME as USERNAME,',
'    REPLACE(PATCHES_V.INSTALL_LOG,''Logged '',''<BR>'') as INSTALL_LOG,',
'    PATCHES_V.CREATED_BY as CREATED_BY,',
'    PATCHES_V.CREATED_ON as CREATED_ON,',
'    PATCHES_V.LAST_UPDATED_BY as LAST_UPDATED_BY,',
'    PATCHES_V.PATCH_TYPE as PATCH_TYPE,',
'    null as dependency,',
'    null as edit,',
'    prereq_count',
' from PATCHES_V '))
,p_plug_source_type=>'NATIVE_IR'
,p_plug_query_options=>'DERIVED_REPORT_COLUMNS'
);
wwv_flow_api.create_worksheet(
 p_id=>wwv_flow_api.id(72821348072899214)
,p_name=>'Installed Patches'
,p_max_row_count=>'1000000'
,p_max_row_count_message=>'The maximum row count for this report is #MAX_ROW_COUNT# rows.  Please apply a filter to reduce the number of records in your query.'
,p_no_data_found_message=>'No data found.'
,p_allow_report_categories=>'N'
,p_show_nulls_as=>'-'
,p_pagination_type=>'ROWS_X_TO_Y'
,p_pagination_display_pos=>'BOTTOM_RIGHT'
,p_report_list_mode=>'TABS'
,p_fixed_header=>'NONE'
,p_show_pivot=>'N'
,p_show_calendar=>'N'
,p_download_formats=>'CSV:HTML:EMAIL'
,p_detail_link_text=>'<img src="#IMAGE_PREFIX#menu/pencil16x16.gif" alt="" />'
,p_icon_view_columns_per_row=>1
,p_owner=>'PETER'
,p_internal_uid=>26511904471117266
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72821459892899217)
,p_db_column_name=>'PATCH_NAME'
,p_display_order=>1
,p_column_identifier=>'A'
,p_column_label=>'Patch Name'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_NAME'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72821551636899217)
,p_db_column_name=>'DB_SCHEMA'
,p_display_order=>2
,p_column_identifier=>'B'
,p_column_label=>'Db Schema'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'DB_SCHEMA'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72821661942899217)
,p_db_column_name=>'BRANCH_NAME'
,p_display_order=>3
,p_column_identifier=>'C'
,p_column_label=>'Branch Name'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'BRANCH_NAME'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72821758952899217)
,p_db_column_name=>'TAG_FROM'
,p_display_order=>4
,p_column_identifier=>'D'
,p_column_label=>'Tag From'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'TAG_FROM'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72821868589899217)
,p_db_column_name=>'TAG_TO'
,p_display_order=>5
,p_column_identifier=>'E'
,p_column_label=>'Tag To'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'TAG_TO'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72821961564899217)
,p_db_column_name=>'SUPPLEMENTARY'
,p_display_order=>6
,p_column_identifier=>'F'
,p_column_label=>'Supplementary'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'SUPPLEMENTARY'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822076295899218)
,p_db_column_name=>'PATCH_DESC'
,p_display_order=>7
,p_column_identifier=>'G'
,p_column_label=>'Patch Desc'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_DESC'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822156276899218)
,p_db_column_name=>'PATCH_COMPONANTS'
,p_display_order=>8
,p_column_identifier=>'H'
,p_column_label=>'Patch Componants'
,p_allow_sorting=>'N'
,p_allow_ctrl_breaks=>'N'
,p_allow_aggregations=>'N'
,p_allow_computations=>'N'
,p_allow_charting=>'N'
,p_allow_group_by=>'N'
,p_allow_pivot=>'N'
,p_column_type=>'CLOB'
,p_display_text_as=>'WITHOUT_MODIFICATION'
,p_heading_alignment=>'LEFT'
,p_static_id=>'PATCH_COMPONANTS'
,p_rpt_show_filter_lov=>'N'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822267533899218)
,p_db_column_name=>'PATCH_CREATE_DATE'
,p_display_order=>9
,p_column_identifier=>'I'
,p_column_label=>'Patch Create Date'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_CREATE_DATE'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822376068899218)
,p_db_column_name=>'PATCH_CREATED_BY'
,p_display_order=>10
,p_column_identifier=>'J'
,p_column_label=>'Patch Created By'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_CREATED_BY'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822460970899218)
,p_db_column_name=>'NOTE'
,p_display_order=>11
,p_column_identifier=>'K'
,p_column_label=>'Note'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'NOTE'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822546132899218)
,p_db_column_name=>'LOG_DATETIME'
,p_display_order=>12
,p_column_identifier=>'L'
,p_column_label=>'Log Datetime'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'LOG_DATETIME'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822646269899218)
,p_db_column_name=>'COMPLETED_DATETIME'
,p_display_order=>13
,p_column_identifier=>'M'
,p_column_label=>'Completed Datetime'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_column_alignment=>'RIGHT'
,p_format_mask=>'DD-MON-YYYY HH24:MI'
,p_tz_dependent=>'N'
,p_static_id=>'COMPLETED_DATETIME'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822746748899218)
,p_db_column_name=>'SUCCESS_YN'
,p_display_order=>14
,p_column_identifier=>'N'
,p_column_label=>'Success Yn'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'SUCCESS_YN'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822867910899219)
,p_db_column_name=>'RETIRED_YN'
,p_display_order=>15
,p_column_identifier=>'O'
,p_column_label=>'Retired Yn'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'RETIRED_YN'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72822974924899219)
,p_db_column_name=>'WARNING_COUNT'
,p_display_order=>16
,p_column_identifier=>'P'
,p_column_label=>'Warning Count'
,p_allow_pivot=>'N'
,p_column_type=>'NUMBER'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'WARNING_COUNT'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72823075594899219)
,p_db_column_name=>'RERUNNABLE_YN'
,p_display_order=>17
,p_column_identifier=>'Q'
,p_column_label=>'Rerunnable Yn'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'RERUNNABLE_YN'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72823160234899219)
,p_db_column_name=>'ERROR_COUNT'
,p_display_order=>18
,p_column_identifier=>'R'
,p_column_label=>'Error Count'
,p_allow_pivot=>'N'
,p_column_type=>'NUMBER'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'ERROR_COUNT'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72823265855899219)
,p_db_column_name=>'USERNAME'
,p_display_order=>19
,p_column_identifier=>'S'
,p_column_label=>'Username'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'USERNAME'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72823375265899219)
,p_db_column_name=>'INSTALL_LOG'
,p_display_order=>20
,p_column_identifier=>'T'
,p_column_label=>'Install Log'
,p_allow_sorting=>'N'
,p_allow_ctrl_breaks=>'N'
,p_allow_aggregations=>'N'
,p_allow_computations=>'N'
,p_allow_charting=>'N'
,p_allow_group_by=>'N'
,p_allow_pivot=>'N'
,p_column_type=>'CLOB'
,p_display_text_as=>'WITHOUT_MODIFICATION'
,p_column_alignment=>'RIGHT'
,p_static_id=>'INSTALL_LOG'
,p_rpt_show_filter_lov=>'N'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72823447588899219)
,p_db_column_name=>'CREATED_BY'
,p_display_order=>21
,p_column_identifier=>'U'
,p_column_label=>'Created By'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'CREATED_BY'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72823545752899219)
,p_db_column_name=>'CREATED_ON'
,p_display_order=>22
,p_column_identifier=>'V'
,p_column_label=>'Created On'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'CREATED_ON'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72823665423899220)
,p_db_column_name=>'LAST_UPDATED_BY'
,p_display_order=>23
,p_column_identifier=>'W'
,p_column_label=>'Last Updated By'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'LAST_UPDATED_BY'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(72823774954899220)
,p_db_column_name=>'PATCH_TYPE'
,p_display_order=>24
,p_column_identifier=>'X'
,p_column_label=>'Patch Type'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_TYPE'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(49159046996845346)
,p_db_column_name=>'DEPENDENCY'
,p_display_order=>25
,p_column_identifier=>'Y'
,p_column_label=>'Dependency'
,p_column_link=>'f?p=&APP_ID.:6:&SESSION.::&DEBUG.::P6_REPORT_SEARCH:#PATCH_NAME#'
,p_column_linktext=>'<img src="#IMAGE_PREFIX#ws/small_page.gif" alt="">'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'DEPENDENCY'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(49511052816860570)
,p_db_column_name=>'EDIT'
,p_display_order=>26
,p_column_identifier=>'Z'
,p_column_label=>'Edit'
,p_column_link=>'f?p=&APP_ID.:7:&SESSION.::&DEBUG.::P7_ROWID:#ROWID#'
,p_column_linktext=>'<img src="#IMAGE_PREFIX#ed-item.gif" alt="">'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'EDIT'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(49515170894897865)
,p_db_column_name=>'ROWID'
,p_display_order=>27
,p_column_identifier=>'AA'
,p_column_label=>'Rowid'
,p_allow_sorting=>'N'
,p_allow_filtering=>'N'
,p_allow_highlighting=>'N'
,p_allow_ctrl_breaks=>'N'
,p_allow_aggregations=>'N'
,p_allow_computations=>'N'
,p_allow_charting=>'N'
,p_allow_group_by=>'N'
,p_allow_pivot=>'N'
,p_column_type=>'OTHER'
,p_display_text_as=>'HIDDEN'
,p_tz_dependent=>'N'
,p_static_id=>'ROWID'
,p_rpt_show_filter_lov=>'N'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(50009047546103263)
,p_db_column_name=>'PREREQ_COUNT'
,p_display_order=>28
,p_column_identifier=>'AB'
,p_column_label=>'Prereq Count'
,p_allow_pivot=>'N'
,p_column_type=>'NUMBER'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'PREREQ_COUNT'
);
wwv_flow_api.create_worksheet_rpt(
 p_id=>wwv_flow_api.id(46380962465590282)
,p_application_user=>'APXWS_ALTERNATIVE'
,p_name=>'Summary'
,p_report_seq=>10
,p_report_alias=>'143269'
,p_status=>'PUBLIC'
,p_is_default=>'Y'
,p_display_rows=>15
,p_report_columns=>'PATCH_NAME:DB_SCHEMA:BRANCH_NAME:TAG_FROM:TAG_TO:SUPPLEMENTARY:PATCH_DESC:PATCH_CREATE_DATE:NOTE:PATCH_TYPE:DEPENDENCY:EDIT:ROWID:PREREQ_COUNT'
,p_sort_column_1=>'PATCH_NAME'
,p_sort_direction_1=>'ASC'
,p_break_on=>'PATCH_TYPE:0:0:0:0:0'
,p_break_enabled_on=>'PATCH_TYPE:0:0:0:0:0'
);
wwv_flow_api.create_worksheet_rpt(
 p_id=>wwv_flow_api.id(46381345065604217)
,p_application_user=>'APXWS_ALTERNATIVE'
,p_name=>'Componants'
,p_report_seq=>10
,p_report_alias=>'143273'
,p_status=>'PUBLIC'
,p_is_default=>'Y'
,p_display_rows=>15
,p_report_columns=>'PATCH_NAME:PATCH_DESC:NOTE:PATCH_COMPONANTS:INSTALL_LOG:PATCH_TYPE:DEPENDENCY:EDIT:ROWID:PREREQ_COUNT'
,p_break_on=>'PATCH_TYPE:0:0:0:0:0'
,p_break_enabled_on=>'PATCH_TYPE:0:0:0:0:0'
);
wwv_flow_api.create_worksheet_rpt(
 p_id=>wwv_flow_api.id(72824559263900523)
,p_application_user=>'APXWS_DEFAULT'
,p_report_seq=>10
,p_report_alias=>'265152'
,p_status=>'PUBLIC'
,p_is_default=>'Y'
,p_display_rows=>15
,p_report_columns=>'PATCH_NAME:PATCH_DESC:PATCH_CREATE_DATE:PATCH_TYPE:COMPLETED_DATETIME:SUCCESS_YN:PREREQ_COUNT:DEPENDENCY:EDIT'
,p_sort_column_1=>'COMPLETED_DATETIME'
,p_sort_direction_1=>'DESC'
,p_sort_column_2=>'0'
,p_sort_direction_2=>'ASC'
,p_sort_column_3=>'0'
,p_sort_direction_3=>'ASC'
,p_sort_column_4=>'0'
,p_sort_direction_4=>'ASC'
,p_sort_column_5=>'0'
,p_sort_direction_5=>'ASC'
,p_sort_column_6=>'0'
,p_sort_direction_6=>'ASC'
,p_break_on=>'PATCH_TYPE:0:0:0:0:0'
,p_break_enabled_on=>'PATCH_TYPE:0:0:0:0:0'
);
wwv_flow_api.create_page_plug(
 p_id=>wwv_flow_api.id(72823970523899221)
,p_plug_name=>'Breadcrumbs'
,p_region_template_options=>'#DEFAULT#:t-BreadcrumbRegion--useBreadcrumbTitle'
,p_component_template_options=>'#DEFAULT#'
,p_plug_template=>wwv_flow_api.id(90119950620216768)
,p_plug_display_sequence=>10
,p_plug_display_point=>'REGION_POSITION_01'
,p_menu_id=>wwv_flow_api.id(72820155995899208)
,p_plug_source_type=>'NATIVE_BREADCRUMB'
,p_menu_template_id=>wwv_flow_api.id(90155628322216800)
);
end;
/
