PROMPT LOG TO TRK-01_05A_05B_PATCH_ADMIN_BACK.log
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

define patch_name = 'TRK-01_05A_05B_PATCH_ADMIN_BACK'
define patch_desc = 'BG-114 Recreate backward link'
define patch_path = 'feature/trk/TRK-01/TRK-01_05A_05B_PATCH_ADMIN_BACK/'
SPOOL TRK-01_05A_05B_PATCH_ADMIN_BACK.log
CONNECT PATCH_ADMIN/&&PATCH_ADMIN_password@&&database
set serveroutput on;
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_05A_05B_PATCH_ADMIN_BACK' -
 ,i_patch_type         => 'feature' -
 ,i_db_schema          => 'PATCH_ADMIN' -
 ,i_branch_name        => 'feature/trk/TRK-01' -
 ,i_tag_from           => 'TRK-01.05A' -
 ,i_tag_to             => 'TRK-01.05B' -
 ,i_supplementary      => 'BACK' -
 ,i_patch_desc         => 'BG-114 Recreate backward link' -
 ,i_patch_componants   => 'patch_admin_backward_dblink.dblink' -
||',create_db_link.prc' -
||',patches_unapplied_v.vw' -
||',components_unapplied_v.vw' -
 ,i_patch_create_date  => '06-12-2016' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => '' -
 ,i_rerunnable_yn      => 'Y' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT Ensure Patch Admin is late enough for this patch
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_05A_05B_PATCH_ADMIN_BACK' -
,i_prereq_patch  => 'TRK-01_01' );
select user||'@'||global_name Connection from global_name;


PROMPT DATABASE LINKS

PROMPT patch_admin_backward_dblink.dblink 
@&&patch_path.patch_admin_backward_dblink.dblink;
Show error;

PROMPT PROCEDURES

PROMPT create_db_link.prc 
@&&patch_path.create_db_link.prc;
Show error;

PROMPT VIEWS

PROMPT patches_unapplied_v.vw 
@&&patch_path.patches_unapplied_v.vw;
Show error;

PROMPT components_unapplied_v.vw 
@&&patch_path.components_unapplied_v.vw;
Show error;

COMMIT;
PROMPT Compiling objects in schema PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;
--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.
execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'TRK-01_05A_05B_PATCH_ADMIN_BACK');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;


COMMIT;

