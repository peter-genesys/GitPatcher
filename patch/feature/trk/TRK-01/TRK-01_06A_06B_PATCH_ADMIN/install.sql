PROMPT LOG TO TRK-01_06A_06B_PATCH_ADMIN.log
PROMPT .
SET AUTOCOMMIT OFF
SET AUTOPRINT ON
SET ECHO ON
SET FEEDBACK ON
SET PAUSE OFF
SET SERVEROUTPUT ON SIZE 1000000
SET TERMOUT ON
SET TRIMOUT ON
SET VERIFY ON
SET trims on pagesize 3000
SET auto off
SET verify off echo off define on
WHENEVER OSERROR EXIT FAILURE ROLLBACK
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

define patch_name = 'TRK-01_06A_06B_PATCH_ADMIN'
define patch_desc = 'Updated Tapis with ROWID'
define patch_path = 'feature/trk/TRK-01/TRK-01_06A_06B_PATCH_ADMIN/'
SPOOL TRK-01_06A_06B_PATCH_ADMIN.log
CONNECT PATCH_ADMIN/&&PATCH_ADMIN_password@&&database
set serveroutput on;
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_06A_06B_PATCH_ADMIN' -
 ,i_patch_type         => 'feature' -
 ,i_db_schema          => 'PATCH_ADMIN' -
 ,i_branch_name        => 'feature/trk/TRK-01' -
 ,i_tag_from           => 'TRK-01.06A' -
 ,i_tag_to             => 'TRK-01.06B' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => 'Updated Tapis with ROWID' -
 ,i_patch_componants   => 'patches.tab' -
||',patch_prereqs.tab' -
||',create_db_link.prc' -
||',patch_installer.pks' -
||',patch_invoker.pks' -
||',text_manip.pks' -
||',patch_prereqs_tapi.pks' -
||',patches_tapi.pks' -
||',components_unapplied_v.vw' -
||',installed_patches_v.vw' -
||',patch_prereqs_v.vw' -
||',patches_components_v.vw' -
||',patches_dependency_v.vw' -
||',patches_unapplied_v.vw' -
||',patches_unpromoted_v.vw' -
||',patches_v.vw' -
||',user_object_dependency_v.vw' -
||',patch_installer.pkb' -
||',text_manip.pkb' -
||',patch_prereqs_tapi.pkb' -
||',patches_tapi.pkb' -
 ,i_patch_create_date  => '02-07-2019' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => '' -
 ,i_rerunnable_yn      => 'Y' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT
PROMPT Checking Prerequisite patch TRK-01_01A_01B_PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_06A_06B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_01A_01B_PATCH_ADMIN' );
PROMPT
PROMPT Checking Prerequisite patch TRK-01_05A_05B_PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_06A_06B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_05A_05B_PATCH_ADMIN' );
PROMPT
PROMPT Checking Prerequisite patch TRK-01_05A_05B_PATCH_ADMIN_BACK
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_06A_06B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_05A_05B_PATCH_ADMIN_BACK' );
PROMPT
PROMPT Checking Prerequisite patch TRK-01_05A_05B_PATCH_ADMIN_FOR
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_06A_06B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_05A_05B_PATCH_ADMIN_FOR' );
PROMPT Ensure Patch Admin is late enough for this patch
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_06A_06B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_01' );
select user||'@'||global_name Connection from global_name;


PROMPT TABLES

WHENEVER SQLERROR CONTINUE
PROMPT patches.tab 
@&&patch_path.patches.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

WHENEVER SQLERROR CONTINUE
PROMPT patch_prereqs.tab 
@&&patch_path.patch_prereqs.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

PROMPT PROCEDURES

PROMPT create_db_link.prc 
@&&patch_path.create_db_link.prc;
Show error;

PROMPT PACKAGE SPECS

PROMPT patch_installer.pks 
@&&patch_path.patch_installer.pks;
Show error;

PROMPT patch_invoker.pks 
@&&patch_path.patch_invoker.pks;
Show error;

PROMPT text_manip.pks 
@&&patch_path.text_manip.pks;
Show error;

PROMPT patch_prereqs_tapi.pks 
@&&patch_path.patch_prereqs_tapi.pks;
Show error;

PROMPT patches_tapi.pks 
@&&patch_path.patches_tapi.pks;
Show error;

PROMPT VIEWS

PROMPT components_unapplied_v.vw 
@&&patch_path.components_unapplied_v.vw;
Show error;

PROMPT installed_patches_v.vw 
@&&patch_path.installed_patches_v.vw;
Show error;

PROMPT patch_prereqs_v.vw 
@&&patch_path.patch_prereqs_v.vw;
Show error;

PROMPT patches_components_v.vw 
@&&patch_path.patches_components_v.vw;
Show error;

PROMPT patches_dependency_v.vw 
@&&patch_path.patches_dependency_v.vw;
Show error;

PROMPT patches_unapplied_v.vw 
@&&patch_path.patches_unapplied_v.vw;
Show error;

PROMPT patches_unpromoted_v.vw 
@&&patch_path.patches_unpromoted_v.vw;
Show error;

PROMPT patches_v.vw 
@&&patch_path.patches_v.vw;
Show error;

PROMPT user_object_dependency_v.vw 
@&&patch_path.user_object_dependency_v.vw;
Show error;

PROMPT PACKAGE BODIES

PROMPT patch_installer.pkb 
@&&patch_path.patch_installer.pkb;
Show error;

PROMPT text_manip.pkb 
@&&patch_path.text_manip.pkb;
Show error;

PROMPT patch_prereqs_tapi.pkb 
@&&patch_path.patch_prereqs_tapi.pkb;
Show error;

PROMPT patches_tapi.pkb 
@&&patch_path.patches_tapi.pkb;
Show error;

COMMIT;
PROMPT Compiling objects in schema PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;
--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.
execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'TRK-01_06A_06B_PATCH_ADMIN');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;


COMMIT;

