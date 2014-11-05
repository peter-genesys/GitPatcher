PROMPT LOG TO TRK-01_01A_01B_PATCH_ADMIN.log
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

define patch_name = 'TRK-01_01A_01B_PATCH_ADMIN'
define patch_desc = 'Install Tracker'
define patch_path = 'feature/trk/TRK-01/TRK-01_01A_01B_PATCH_ADMIN/'
SPOOL TRK-01_01A_01B_PATCH_ADMIN.log
CONNECT &&PATCH_ADMIN_user/&&PATCH_ADMIN_password@&&database
set serveroutput on;
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_01A_01B_PATCH_ADMIN' -
 ,i_patch_type         => 'feature' -
 ,i_db_schema          => '&&PATCH_ADMIN_user' -
 ,i_branch_name        => 'feature/trk/TRK-01' -
 ,i_tag_from           => 'TRK-01.01A' -
 ,i_tag_to             => 'TRK-01.01B' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => 'Install Tracker' -
 ,i_patch_componants   => 'patch_prereqs.tab' -
||',patch_supersedes.tab' -
||',patches.tab' -
||',patch_admin_backward_dblink.dblink' -
||',patch_admin_forward_dblink.dblink' -
||',patch_installer.pks' -
||',patch_invoker.pks' -
||',patch_prereqs_tapi.pks' -
||',patch_supersedes_tapi.pks' -
||',patches_tapi.pks' -
||',text_manip.pks' -
||',components_unapplied_v.vw' -
||',patch_prereqs_v.vw' -
||',patch_supersedes_v.vw' -
||',patches_components_v.vw' -
||',patches_dependency_v.vw' -
||',patches_unapplied_v.vw' -
||',patches_unpromoted_v.vw' -
||',patches_v.vw' -
||',user_object_dependency_v.vw' -
||',end_user.grt' -
||',patch_installer.pkb' -
||',patch_invoker.pkb' -
||',patch_prereqs_tapi.pkb' -
||',patch_supersedes_tapi.pkb' -
||',patches_tapi.pkb' -
||',text_manip.pkb' -
||',patch_prereqs_aud.trg' -
||',patch_supersedes_aud.trg' -
||',patches_aud.trg' -
||',patches_biur.trg' -
 ,i_patch_create_date  => '11-05-2014' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => '' -
 ,i_rerunnable_yn      => 'Y' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT
PROMPT Checking Prerequisite patch TRK-01_01A_01B_SYS
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_01A_01B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_01A_01B_SYS' );

select user||'@'||global_name Connection from global_name;


PROMPT TABLES

WHENEVER SQLERROR CONTINUE
PROMPT patch_prereqs.tab 
@&&patch_path.patch_prereqs.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes.tab 
@&&patch_path.patch_supersedes.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

WHENEVER SQLERROR CONTINUE
PROMPT patches.tab 
@&&patch_path.patches.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

PROMPT DATABASE LINKS

PROMPT patch_admin_backward_dblink.dblink 
@&&patch_path.patch_admin_backward_dblink.dblink;
Show error;

PROMPT patch_admin_forward_dblink.dblink 
@&&patch_path.patch_admin_forward_dblink.dblink;
Show error;

PROMPT PACKAGE SPECS

PROMPT patch_installer.pks 
@&&patch_path.patch_installer.pks;
Show error;

PROMPT patch_invoker.pks 
@&&patch_path.patch_invoker.pks;
Show error;

PROMPT patch_prereqs_tapi.pks 
@&&patch_path.patch_prereqs_tapi.pks;
Show error;

PROMPT patch_supersedes_tapi.pks 
@&&patch_path.patch_supersedes_tapi.pks;
Show error;

PROMPT patches_tapi.pks 
@&&patch_path.patches_tapi.pks;
Show error;

PROMPT text_manip.pks 
@&&patch_path.text_manip.pks;
Show error;

PROMPT VIEWS

PROMPT components_unapplied_v.vw 
@&&patch_path.components_unapplied_v.vw;
Show error;

PROMPT patch_prereqs_v.vw 
@&&patch_path.patch_prereqs_v.vw;
Show error;

PROMPT patch_supersedes_v.vw 
@&&patch_path.patch_supersedes_v.vw;
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

PROMPT GRANTS

PROMPT end_user.grt 
@&&patch_path.end_user.grt;

PROMPT PACKAGE BODIES

PROMPT patch_installer.pkb 
@&&patch_path.patch_installer.pkb;
Show error;

PROMPT patch_invoker.pkb 
@&&patch_path.patch_invoker.pkb;
Show error;

PROMPT patch_prereqs_tapi.pkb 
@&&patch_path.patch_prereqs_tapi.pkb;
Show error;

PROMPT patch_supersedes_tapi.pkb 
@&&patch_path.patch_supersedes_tapi.pkb;
Show error;

PROMPT patches_tapi.pkb 
@&&patch_path.patches_tapi.pkb;
Show error;

PROMPT text_manip.pkb 
@&&patch_path.text_manip.pkb;
Show error;

PROMPT TRIGGERS

PROMPT patch_prereqs_aud.trg 
@&&patch_path.patch_prereqs_aud.trg;
Show error;

PROMPT patch_supersedes_aud.trg 
@&&patch_path.patch_supersedes_aud.trg;
Show error;

PROMPT patches_aud.trg 
@&&patch_path.patches_aud.trg;
Show error;

PROMPT patches_biur.trg 
@&&patch_path.patches_biur.trg;
Show error;

COMMIT;
PROMPT Compiling objects in schema PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;
--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.
execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'TRK-01_01A_01B_PATCH_ADMIN');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;


COMMIT;

