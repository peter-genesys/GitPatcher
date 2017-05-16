CREATE OR REPLACE PACKAGE patch_installer AS
  --------------------------------------------------------------
  -- NAME   : patch_installer
  -- PURPOSE: built in patch management software for 
  -- $ Id: $
  --------------------------------------------------------------
 
 
  TYPE patches_tab IS table of patches%ROWTYPE;
  TYPE patch_components_tab IS table of VARCHAR2(100);
 
---------------------------------------------------------------------------
--f_warning_count
--------------------------------------------------------------------------- 
FUNCTION f_warning_count return NUMBER;

---------------------------------------------------------------------------
--f_error_count
--------------------------------------------------------------------------- 
FUNCTION f_error_count return NUMBER;


---------------------------------------------------------------------------
--f_patches
--------------------------------------------------------------------------- 
FUNCTION f_patches return patches%ROWTYPE;

  --------------------------------------------------------------
  -- reset_patch
  --------------------------------------------------------------
  PROCEDURE reset_patch;
 
  --------------------------------------------------------------
  -- do_sql_with_warning
  --------------------------------------------------------------
  PROCEDURE do_sql_with_warning(i_sql IN VARCHAR2);
  --------------------------------------------------------------
  -- do_sql_with_error
  --------------------------------------------------------------
  PROCEDURE do_sql_with_error(i_sql IN VARCHAR2);
  
  PROCEDURE alert_warning(i_message IN VARCHAR2 DEFAULT NULL);
  
  PROCEDURE alert_error(i_message IN VARCHAR2 DEFAULT NULL);
 
  --------------------------------------------------------------
  -- patch_started
  --------------------------------------------------------------
  PROCEDURE patch_started( i_patch_name        IN VARCHAR2
                          ,i_patch_type        IN VARCHAR2
                          ,i_db_schema         IN VARCHAR2
                          ,i_branch_name       IN VARCHAR2
                          ,i_tag_from          IN VARCHAR2
                          ,i_tag_to            IN VARCHAR2
                          ,i_supplementary     IN VARCHAR2
                          ,i_patch_desc        IN VARCHAR2
                          ,i_patch_componants  IN VARCHAR2
                          ,i_patch_create_date IN VARCHAR2
                          ,i_patch_created_by  IN VARCHAR2  
                          ,i_note              IN VARCHAR2
                          ,i_rerunnable_yn     IN VARCHAR2
                          ,i_remove_prereqs    IN VARCHAR2 DEFAULT 'N'
                          ,i_remove_sups       IN VARCHAR2 DEFAULT 'N'
                          ,i_track_promotion   IN VARCHAR2 DEFAULT 'Y'
                          ,i_retired_yn        IN VARCHAR2 DEFAULT 'N'
                          );
 
  
  --------------------------------------------------------------
  -- list_invalid_schema_objects
  --------------------------------------------------------------
  PROCEDURE list_invalid_schema_objects(i_schema IN VARCHAR2 DEFAULT '%');
  --------------------------------------------------------------
  -- count_invalid_schema_objects
  --------------------------------------------------------------
  PROCEDURE count_invalid_schema_objects(i_schema IN VARCHAR2 DEFAULT '%');
  --------------------------------------------------------------
  -- compile_schema
  --------------------------------------------------------------
  PROCEDURE compile_schema(i_schema IN VARCHAR2 DEFAULT '%');
 
  --------------------------------------------------------------
  -- patch_log
  --------------------------------------------------------------
  --PROCEDURE patch_log(i_log                 IN VARCHAR2  );
 
  --------------------------------------------------------------
  -- patch_completed
  --------------------------------------------------------------
  PROCEDURE patch_completed(i_patch_name IN VARCHAR2 DEFAULT NULL);
 
  --------------------------------------------------------------
  -- get_dba_objects
  --------------------------------------------------------------
  FUNCTION get_dba_objects(i_owner       IN VARCHAR2
                          ,i_object_name IN VARCHAR2
                          ,i_object_type IN VARCHAR2) RETURN dba_objects%ROWTYPE;
 
  --------------------------------------------------------------
  -- get_patches
  --------------------------------------------------------------
  FUNCTION get_patches(i_patch_name       IN VARCHAR2  ) RETURN patches%ROWTYPE;
 
-----------------------------------------------------------------
-- is_prereq_patch - is this patch a prereq for another patch.
-----------------------------------------------------------------
FUNCTION is_prereq_patch (
   i_prereq_patch IN PATCH_PREREQS.PREREQ_PATCH%TYPE ) RETURN BOOLEAN;

-----------------------------------------------------------------
-- is_superseded_patch - is this patch superseded by another patch.
-----------------------------------------------------------------
FUNCTION is_superseded_patch (
   i_supersedes_patch IN patch_supersedes.supersedes_patch%TYPE ) RETURN BOOLEAN;
  --------------------------------------------------------------
  -- add_patch_supersedes - deprecated
  --------------------------------------------------------------
  PROCEDURE add_patch_supersedes( i_patch_name       IN VARCHAR2
                                 ,i_supersedes_patch IN VARCHAR2 );
  
  --------------------------------------------------------------
  -- add_patch_prereq
  --------------------------------------------------------------
  PROCEDURE add_patch_prereq( i_patch_name   IN VARCHAR2
                             ,i_prereq_patch IN VARCHAR2 );
          
--------------------------------------------------------------------
--patch_dependency_tab
--returns PIPELINED patches_tab - patches in install order
-------------------------------------------------------------------- 

FUNCTION patch_dependency_tab RETURN patches_tab PIPELINED;
          
 --------------------------------------------------------------------
 --patch_dependency
 --returns dbms_output list of patches in install order.
 -------------------------------------------------------------------- 
 
 PROCEDURE patch_dependency;            

--------------------------------------------------------------------
--get_patch_component_tab
--returns patch_components_tab - patches as components
-------------------------------------------------------------------- 

FUNCTION get_patch_component_tab(i_patch_name IN VARCHAR2) RETURN patch_components_tab;

--------------------------------------------------------------------
--patch_component_tab
--returns PIPELINED patch_components_tab - patches as components
-------------------------------------------------------------------- 

FUNCTION patch_component_tab(i_patch_name IN VARCHAR2) RETURN patch_components_tab PIPELINED;


--------------------------------------------------------------------
--patch_component
--returns dbms_output list of - patches as components
-------------------------------------------------------------------- 

PROCEDURE patch_component(i_patch_name IN VARCHAR2);
                             
 
--------------------------------------------------------------------
--get_last_patch
--returns the patch_name of the last patch that installed the patch_component
-------------------------------------------------------------------- 

FUNCTION get_last_patch(i_patch_component IN VARCHAR2) RETURN VARCHAR2; 
 
END;
/

