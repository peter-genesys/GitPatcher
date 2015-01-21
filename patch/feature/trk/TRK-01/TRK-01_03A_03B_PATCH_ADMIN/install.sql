PROMPT LOG TO TRK-01_03A_03B_PATCH_ADMIN.log
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

define patch_name = 'TRK-01_03A_03B_PATCH_ADMIN'
define patch_desc = 'Adjust unpromoted view'
define patch_path = 'feature/trk/TRK-01/TRK-01_03A_03B_PATCH_ADMIN/'
SPOOL TRK-01_03A_03B_PATCH_ADMIN.log
CONNECT &&PATCH_ADMIN_user/&&PATCH_ADMIN_password@&&database
set serveroutput on;
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_03A_03B_PATCH_ADMIN' -
 ,i_patch_type         => 'feature' -
 ,i_db_schema          => '&&PATCH_ADMIN_user' -
 ,i_branch_name        => 'feature/trk/TRK-01' -
 ,i_tag_from           => 'TRK-01.03A' -
 ,i_tag_to             => 'TRK-01.03B' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => 'Adjust unpromoted view' -
 ,i_patch_componants   => 'patches_unpromoted_v.vw' -
 ,i_patch_create_date  => '01-21-2015' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => '' -
 ,i_rerunnable_yn      => 'Y' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT Ensure Patch Admin is late enough for this patch
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_03A_03B_PATCH_ADMIN' -
,i_prereq_patch  => 'TRK-01_01' );
select user||'@'||global_name Connection from global_name;


PROMPT VIEWS

PROMPT patches_unpromoted_v.vw 
@&&patch_path.patches_unpromoted_v.vw;
Show error;

COMMIT;
PROMPT Compiling objects in schema PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;
--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.
execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'TRK-01_03A_03B_PATCH_ADMIN');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;


COMMIT;

