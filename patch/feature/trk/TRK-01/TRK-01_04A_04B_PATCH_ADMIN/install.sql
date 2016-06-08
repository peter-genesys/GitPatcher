PROMPT LOG TO TRK-01_04A_04B_PATCH_ADMIN.log
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

define patch_name = 'TRK-01_04A_04B_PATCH_ADMIN'
define patch_desc = 'Fixing Infinite Recursion from superseded patch.'
define patch_path = 'feature/trk/TRK-01/TRK-01_04A_04B_PATCH_ADMIN/'
SPOOL TRK-01_04A_04B_PATCH_ADMIN.log
CONNECT PATCH_ADMIN/&&PATCH_ADMIN_password@&&database
set serveroutput on;
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_04A_04B_PATCH_ADMIN' -
 ,i_patch_type         => 'feature' -
 ,i_db_schema          => 'PATCH_ADMIN' -
 ,i_branch_name        => 'feature/trk/TRK-01' -
 ,i_tag_from           => 'TRK-01.04A' -
 ,i_tag_to             => 'TRK-01.04B' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => 'Fixing Infinite Recursion from superseded patch.' -
 ,i_patch_componants   => 'patches_v.vw' -
||',patch_installer.pkb' -
 ,i_patch_create_date  => '03-02-2015' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => 'This is a stop gap - real solution is probably to disable supsersedes altogether.' -
 ,i_rerunnable_yn      => 'Y' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT
PROMPT Checking Prerequisite patch TRK-01_01A_01B_PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_04A_04B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_01A_01B_PATCH_ADMIN' );
PROMPT Ensure Patch Admin is late enough for this patch
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_04A_04B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_01' );
select user||'@'||global_name Connection from global_name;


PROMPT VIEWS

PROMPT patches_v.vw 
@&&patch_path.patches_v.vw;
Show error;

PROMPT PACKAGE BODIES

PROMPT patch_installer.pkb 
@&&patch_path.patch_installer.pkb;
Show error;

COMMIT;
PROMPT Compiling objects in schema PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;
--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.
execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'TRK-01_04A_04B_PATCH_ADMIN');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;


COMMIT;

