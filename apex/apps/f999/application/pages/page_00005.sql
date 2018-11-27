prompt --application/pages/page_00005
begin
wwv_flow_api.create_page(
 p_id=>5
,p_user_interface_id=>wwv_flow_api.id(72818849918899195)
,p_tab_set=>'TS1'
,p_name=>'Patches Dependency'
,p_step_title=>'Patches Dependency'
,p_reload_on_submit=>'A'
,p_warn_on_unsaved_changes=>'N'
,p_step_sub_title=>'Patches Dependency'
,p_step_sub_title_type=>'TEXT_WITH_SUBSTITUTIONS'
,p_autocomplete_on_off=>'ON'
,p_page_template_options=>'#DEFAULT#'
,p_nav_list_template_options=>'#DEFAULT#'
,p_help_text=>'No help is available for this page.'
,p_last_updated_by=>'BURGESPE'
,p_last_upd_yyyymmddhh24miss=>'20170503160645'
);
wwv_flow_api.create_page_plug(
 p_id=>wwv_flow_api.id(48527358885579940)
,p_plug_name=>'Installed Patches'
,p_region_template_options=>'#DEFAULT#:t-Region--scrollBody'
,p_component_template_options=>'#DEFAULT#'
,p_plug_template=>wwv_flow_api.id(90112942111216764)
,p_plug_display_sequence=>10
,p_plug_display_point=>'BODY_3'
,p_query_type=>'SQL'
,p_plug_source=>wwv_flow_string.join(wwv_flow_t_varchar2(
'select v.*',
',decode(patch_type,''patchset'','''',REPLACE(v.PATCH_COMPONANTS,'','',''<BR>'')) as PATCH_COMPONANTS_LIST',
',decode(patch_type,''feature'','''',''hotfix'','''',REPLACE(PATCH_COMPONANTS,'','',''<BR>''))   SUBPATCHES',
',null dependency                     ',
' from PATCHES_DEPENDENCY_V v'))
,p_plug_source_type=>'NATIVE_IR'
);
wwv_flow_api.create_worksheet(
 p_id=>wwv_flow_api.id(48527556679579948)
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
,p_show_detail_link=>'N'
,p_show_pivot=>'N'
,p_show_calendar=>'N'
,p_download_formats=>'CSV:HTML:EMAIL'
,p_detail_link_text=>'<img src="#IMAGE_PREFIX#menu/pencil16x16.gif" alt="" />'
,p_allow_exclude_null_values=>'N'
,p_allow_hide_extra_columns=>'N'
,p_icon_view_columns_per_row=>1
,p_owner=>'PETER'
,p_internal_uid=>16473417373231878
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(48527645492579955)
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
 p_id=>wwv_flow_api.id(48527759510579958)
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
 p_id=>wwv_flow_api.id(48527866317579958)
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
 p_id=>wwv_flow_api.id(48527943538579959)
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
 p_id=>wwv_flow_api.id(48528059962579959)
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
 p_id=>wwv_flow_api.id(48528158513579959)
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
 p_id=>wwv_flow_api.id(48528241116579959)
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
 p_id=>wwv_flow_api.id(48528346075579959)
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
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_COMPONANTS'
,p_rpt_show_filter_lov=>'N'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(48528453017579959)
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
 p_id=>wwv_flow_api.id(48528564258579960)
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
 p_id=>wwv_flow_api.id(48528655546579960)
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
 p_id=>wwv_flow_api.id(48528742965579960)
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
 p_id=>wwv_flow_api.id(48528848102579960)
,p_db_column_name=>'COMPLETED_DATETIME'
,p_display_order=>13
,p_column_identifier=>'M'
,p_column_label=>'Completed Datetime'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'COMPLETED_DATETIME'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(48528962743579960)
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
 p_id=>wwv_flow_api.id(48529056109579960)
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
 p_id=>wwv_flow_api.id(48529169792579960)
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
 p_id=>wwv_flow_api.id(48529258078579961)
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
 p_id=>wwv_flow_api.id(48529360637579961)
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
 p_id=>wwv_flow_api.id(48529454102579961)
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
 p_id=>wwv_flow_api.id(48529549888579961)
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
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'INSTALL_LOG'
,p_rpt_show_filter_lov=>'N'
,p_help_text=>'No help available for this page item.'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(48529651286579961)
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
 p_id=>wwv_flow_api.id(48529749790579961)
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
 p_id=>wwv_flow_api.id(48529848825579962)
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
 p_id=>wwv_flow_api.id(48529960185579962)
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
 p_id=>wwv_flow_api.id(48531270595649205)
,p_db_column_name=>'PATCH_ID'
,p_display_order=>25
,p_column_identifier=>'Y'
,p_column_label=>'Patch Id'
,p_allow_pivot=>'N'
,p_column_type=>'NUMBER'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_ID'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(48531342445649207)
,p_db_column_name=>'LAST_UPDATED_ON'
,p_display_order=>26
,p_column_identifier=>'Z'
,p_column_label=>'Last Updated On'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_tz_dependent=>'N'
,p_static_id=>'LAST_UPDATED_ON'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(48531452409649208)
,p_db_column_name=>'SUBPATCHES'
,p_display_order=>27
,p_column_identifier=>'AA'
,p_column_label=>'Subpatches'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_display_text_as=>'WITHOUT_MODIFICATION'
,p_tz_dependent=>'N'
,p_static_id=>'SUBPATCHES'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(49177350792949887)
,p_db_column_name=>'DEPENDENCY'
,p_display_order=>28
,p_column_identifier=>'AB'
,p_column_label=>'Dependency'
,p_column_link=>'f?p=&APP_ID.:6:&SESSION.::&DEBUG.::P6_REPORT_SEARCH:#PATCH_NAME#'
,p_column_linktext=>'<img src="#IMAGE_PREFIX#ws/small_page.gif" alt="">'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'DEPENDENCY'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(49183062173005390)
,p_db_column_name=>'PATCH_COMPONANTS_LIST'
,p_display_order=>29
,p_column_identifier=>'AC'
,p_column_label=>'Patch Componants List'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_display_text_as=>'WITHOUT_MODIFICATION'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_COMPONANTS_LIST'
);
wwv_flow_api.create_worksheet_rpt(
 p_id=>wwv_flow_api.id(48530068196579964)
,p_application_user=>'APXWS_ALTERNATIVE'
,p_name=>'Summary'
,p_report_seq=>10
,p_report_alias=>'164760'
,p_status=>'PUBLIC'
,p_is_default=>'Y'
,p_display_rows=>15
,p_report_columns=>'PATCH_NAME:DB_SCHEMA:BRANCH_NAME:TAG_FROM:TAG_TO:SUPPLEMENTARY:PATCH_DESC:PATCH_CREATE_DATE:NOTE:PATCH_TYPE:SUBPATCHES:DEPENDENCY:PATCH_COMPONANTS_LIST'
,p_sort_column_1=>'PATCH_NAME'
,p_sort_direction_1=>'ASC'
,p_break_on=>'PATCH_TYPE:0:0:0:0:0'
,p_break_enabled_on=>'PATCH_TYPE:0:0:0:0:0'
);
wwv_flow_api.create_worksheet_rpt(
 p_id=>wwv_flow_api.id(48530256027579970)
,p_application_user=>'APXWS_ALTERNATIVE'
,p_name=>'Componants'
,p_report_seq=>10
,p_report_alias=>'164762'
,p_status=>'PUBLIC'
,p_is_default=>'Y'
,p_display_rows=>15
,p_report_columns=>'PATCH_NAME:PATCH_DESC:NOTE:INSTALL_LOG:PATCH_TYPE:PATCH_COMPONANTS_LIST:SUBPATCHES:DEPENDENCY'
,p_break_on=>'PATCH_TYPE:0:0:0:0:0'
,p_break_enabled_on=>'PATCH_TYPE:0:0:0:0:0'
);
wwv_flow_api.create_worksheet_rpt(
 p_id=>wwv_flow_api.id(48530459914579970)
,p_application_user=>'APXWS_DEFAULT'
,p_report_seq=>10
,p_report_alias=>'164764'
,p_status=>'PUBLIC'
,p_is_default=>'Y'
,p_report_columns=>'PATCH_NAME:PATCH_DESC:PATCH_CREATE_DATE:PATCH_TYPE:COMPLETED_DATETIME:SUCCESS_YN:SUBPATCHES:DEPENDENCY:PATCH_COMPONANTS_LIST'
,p_break_on=>'PATCH_TYPE:0:0:0:0:0'
,p_break_enabled_on=>'0:0:0:0:0:PATCH_TYPE'
);
wwv_flow_api.create_page_plug(
 p_id=>wwv_flow_api.id(48530656207579973)
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
