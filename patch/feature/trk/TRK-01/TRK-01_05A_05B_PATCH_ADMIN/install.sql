PROMPT LOG TO TRK-01_05A_05B_PATCH_ADMIN.log
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

define patch_name = 'TRK-01_05A_05B_PATCH_ADMIN'
define patch_desc = 'BG-114 Tuning PATCH_ADMIN'
define patch_path = 'feature/trk/TRK-01/TRK-01_05A_05B_PATCH_ADMIN/'
SPOOL TRK-01_05A_05B_PATCH_ADMIN.log
CONNECT PATCH_ADMIN/&&PATCH_ADMIN_password@&&database
set serveroutput on;
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_05A_05B_PATCH_ADMIN' -
 ,i_patch_type         => 'feature' -
 ,i_db_schema          => 'PATCH_ADMIN' -
 ,i_branch_name        => 'feature/trk/TRK-01' -
 ,i_tag_from           => 'TRK-01.05A' -
 ,i_tag_to             => 'TRK-01.05B' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => '' -
 ,i_patch_componants   => 'patch_supersedes.tab' -
||',patches.tab' -
||',create_db_link.prc' -
||',patch_installer.pks' -
||',patch_supersedes_tapi.pks' -
||',patch_invoker.pks' -
||',patch_prereqs_tapi.pks' -
||',patches_tapi.pks' -
||',text_manip.pks' -
||',installed_patches_v.vw' -
||',patch_supersedes_v.vw' -
||',patches_v.vw' -
||',patch_installer.pkb' -
||',patch_supersedes_tapi.pkb' -
||',patches_tapi.pkb' -
||',patch_invoker.pkb' -
||',patch_prereqs_tapi.pkb' -
||',text_manip.pkb' -
||',patches_biur.trg' -
 ,i_patch_create_date  => '06-17-2016' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => 'Apply to all DBs, then apply BACK and FOR patches' -
 ,i_rerunnable_yn      => 'Y' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT
PROMPT Checking Prerequisite patch TRK-01_01A_01B_PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_05A_05B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_01A_01B_PATCH_ADMIN' );
PROMPT
PROMPT Checking Prerequisite patch TRK-01_04A_04B_PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_05A_05B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_04A_04B_PATCH_ADMIN' );
PROMPT Ensure Patch Admin is late enough for this patch
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_05A_05B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_01' );
select user||'@'||global_name Connection from global_name;


PROMPT TABLES

WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes.tab 
@&&patch_path.patch_supersedes.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

WHENEVER SQLERROR CONTINUE
PROMPT patches.tab 
@&&patch_path.patches.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

PROMPT PROCEDURES

PROMPT create_db_link.prc 
@&&patch_path.create_db_link.prc;
Show error;

PROMPT PACKAGE SPECS


WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes_tapi.pks 
@&&patch_path.patch_supersedes_tapi.pks;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK
Show error;

PROMPT patch_invoker.pks 
@&&patch_path.patch_invoker.pks;
Show error;

PROMPT patch_prereqs_tapi.pks 
@&&patch_path.patch_prereqs_tapi.pks;
Show error;

PROMPT patches_tapi.pks 
@&&patch_path.patches_tapi.pks;
Show error;

PROMPT text_manip.pks 
@&&patch_path.text_manip.pks;
Show error;

PROMPT VIEWS

PROMPT installed_patches_v.vw 
@&&patch_path.installed_patches_v.vw;
Show error;

WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes_v.vw 
@&&patch_path.patch_supersedes_v.vw;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK
Show error;

PROMPT patches_v.vw 
@&&patch_path.patches_v.vw;
Show error;

PROMPT PACKAGE BODIES



WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes_tapi.pkb 
@&&patch_path.patch_supersedes_tapi.pkb;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK
Show error;

PROMPT patches_tapi.pkb 
@&&patch_path.patches_tapi.pkb;
Show error;

PROMPT patch_invoker.pkb 
@&&patch_path.patch_invoker.pkb;
Show error;

PROMPT patch_prereqs_tapi.pkb 
@&&patch_path.patch_prereqs_tapi.pkb;
Show error;

PROMPT text_manip.pkb 
@&&patch_path.text_manip.pkb;
Show error;

PROMPT TRIGGERS

PROMPT patches_biur.trg 
@&&patch_path.patches_biur.trg;
Show error;

COMMIT;
PROMPT Compiling objects in schema PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;
--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.
execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'TRK-01_05A_05B_PATCH_ADMIN');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;


PROMPT POST COMPLETION
PROMPT patch_installer.pks 
@&&patch_path.patch_installer.pks;
Show error;

PROMPT patch_installer.pkb 
@&&patch_path.patch_installer.pkb;
Show error;
COMMIT;

