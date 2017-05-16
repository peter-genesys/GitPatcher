PROMPT LOG TO SS-001_01.log
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

define patch_name = 'SS-001_01'
define patch_desc = 'Simple single schema install'
define patch_path = 'feature/ss/SS-001/SS-001_01/'
SPOOL SS-001_01.log
CONNECT &&SIMPLE_user/&&SIMPLE_password@&&database
set serveroutput on;
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_01' -
 ,i_patch_type         => 'dummy' -
 ,i_db_schema          => '&&SIMPLE_user' -
 ,i_branch_name        => 'release/patchset/trk/TRK-01_01' -
 ,i_tag_from           => 'TRK-01.01A' -
 ,i_tag_to             => 'TRK-01.01B' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => 'Full Tracker install' -
 ,i_patch_componants   => '' -
 ,i_patch_create_date  => '11-05-2014' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => '' -
 ,i_rerunnable_yn      => 'N' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'TRK-01_01');


execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'SS-001_01' -
 ,i_patch_type         => 'feature' -
 ,i_db_schema          => '&&SIMPLE_user' -
 ,i_branch_name        => 'feature/ss/SS-001' -
 ,i_tag_from           => 'SS-001.01A' -
 ,i_tag_to             => 'SS-001.01B' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => 'Simple single schema install' -
 ,i_patch_componants   => 'patches.tab' -
||',patch_prereqs.tab' -
||',patch_installer.pks' -
||',patch_invoker.pks' -
||',text_manip.pks' -
||',patches_v.vw' -
||',dba_objects_v.vw' -
||',installed_patches_v.vw' -
||',patch_prereqs_v.vw' -
||',patches_components_v.vw' -
||',patches_dependency_v.vw' -
||',patches_unapplied_v.vw' -
||',patches_unpromoted_v.vw' -
||',components_unapplied_v.vw' -
||',user_object_dependency_v.vw' -
||',patch_installer.pkb' -
||',patch_invoker.pkb' -
||',text_manip.pkb' -
||',patch_prereqs_aud.trg' -
||',patches_aud.trg' -
||',patches_biur.trg' -
 ,i_patch_create_date  => '05-16-2017' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => '' -
 ,i_rerunnable_yn      => 'Y' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT Ensure Patch Admin is late enough for this patch
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'SS-001_01' -
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

PROMPT VIEWS

PROMPT patches_v.vw 
@&&patch_path.patches_v.vw;
Show error;

PROMPT dba_objects_v.vw 
@&&patch_path.dba_objects_v.vw;
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

PROMPT components_unapplied_v.vw 
@&&patch_path.components_unapplied_v.vw;
Show error;

PROMPT user_object_dependency_v.vw 
@&&patch_path.user_object_dependency_v.vw;
Show error;

PROMPT PACKAGE BODIES

PROMPT patch_installer.pkb 
@&&patch_path.patch_installer.pkb;
Show error;

PROMPT patch_invoker.pkb 
@&&patch_path.patch_invoker.pkb;
Show error;

PROMPT text_manip.pkb 
@&&patch_path.text_manip.pkb;
Show error;

PROMPT TRIGGERS

PROMPT patch_prereqs_aud.trg 
@&&patch_path.patch_prereqs_aud.trg;
Show error;

PROMPT patches_aud.trg 
@&&patch_path.patches_aud.trg;
Show error;

PROMPT patches_biur.trg 
@&&patch_path.patches_biur.trg;
Show error;

COMMIT;
PROMPT Compiling objects in schema &&SIMPLE_user
execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;
--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.
execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'SS-001_01');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;


COMMIT;

