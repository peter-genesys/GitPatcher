CREATE OR REPLACE PACKAGE BODY patch_installer AS
  --------------------------------------------------------------
  -- NAME   : patch_installer
  -- PURPOSE: built in patch management software for 
  -- $ Id: $
  --------------------------------------------------------------
  g_warning_count NUMBER := 0;
  g_error_count   NUMBER := 0;
  g_patches       patches%ROWTYPE;
  
  G_DATETIME_MASK CONSTANT VARCHAR2(30) := 'DD-MON-YYYY HH24:mi';
  
---------------------------------------------------------------------------
--f_warning_count
--------------------------------------------------------------------------- 
FUNCTION f_warning_count return NUMBER IS
BEGIN
  RETURN g_warning_count;
END;


---------------------------------------------------------------------------
--f_error_count
--------------------------------------------------------------------------- 
FUNCTION f_error_count return NUMBER IS
BEGIN
  RETURN g_error_count;
END;

---------------------------------------------------------------------------
--f_patches
--------------------------------------------------------------------------- 
FUNCTION f_patches return patches%ROWTYPE IS
BEGIN
  RETURN g_patches;
END;
  
---------------------------------------------------------------------------
--dbms_output feedback.
---------------------------------------------------------------------------  
 
PROCEDURE put_lines(i_line IN VARCHAR2 ) IS
--Divide i_line into a number of 100 char lines for easy display and output each.

  l_line VARCHAR2(32000)     := i_line;
  G_LINESIZE CONSTANT NUMBER := 100;
BEGIN
  WHILE l_line IS NOT NULL LOOP                        --Loop until l_line is empty.
    dbms_output.put_line(SUBSTR(l_line,1,G_LINESIZE)); --output left-most chars (upto G_LINESIZE chars)
    l_line := SUBSTR(l_line,G_LINESIZE+1);             --shorten l_line by G_LINESIZE chars from the left.
  END LOOP;

END; 
  
 
  
  --------------------------------------------------------------
  -- reset_patch
  --------------------------------------------------------------
  PROCEDURE reset_patch IS
  BEGIN                  
    g_warning_count      := 0;
    g_error_count        := 0;
    g_patches.patch_id   := NULL;
  END;
  
  PROCEDURE alert_warning(i_message IN VARCHAR2 DEFAULT NULL) IS
  BEGIN
      g_warning_count := g_warning_count + 1;
      put_lines('WARNING ('||g_warning_count||'): '||i_message);
  END;    
  
  PROCEDURE alert_error(i_message IN VARCHAR2 DEFAULT NULL) IS
  BEGIN
      g_error_count := g_error_count + 1;
      put_lines('ERROR('||g_error_count||'): '||i_message);
  END;    
  
  --------------------------------------------------------------
  -- do_sql_with_warning
  --------------------------------------------------------------
  PROCEDURE do_sql_with_warning(i_sql IN VARCHAR2) IS
  BEGIN
    put_lines(i_sql);
    EXECUTE IMMEDIATE i_sql;
    put_lines('SUCCESS.');
  EXCEPTION
    WHEN OTHERS THEN
      alert_warning;
      put_lines(SQLERRM);
      put_lines('NOT DONE.');
  END;
  --------------------------------------------------------------
  -- do_sql_with_error
  --------------------------------------------------------------
  PROCEDURE do_sql_with_error(i_sql IN VARCHAR2) IS
  BEGIN
    put_lines(i_sql);
    EXECUTE IMMEDIATE i_sql;
    put_lines('SUCCESS.');
  EXCEPTION
    WHEN OTHERS THEN
      alert_error;
      put_lines(SQLERRM);
      put_lines('FAILURE.');
  END;
  
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
                          ) IS
                         
    l_patch patches%ROWTYPE;
    
    PRAGMA AUTONOMOUS_TRANSACTION;
  BEGIN
    reset_patch;
 
 
    l_patch.patch_name         := i_patch_name       ;
    l_patch.patch_type         := i_patch_type       ;
    l_patch.db_schema          := i_db_schema        ;
    l_patch.branch_name        := i_branch_name      ;
    l_patch.tag_from           := i_tag_from         ;
    l_patch.tag_to             := i_tag_to           ;
    l_patch.supplementary      := i_supplementary    ;
    l_patch.patch_desc         := i_patch_desc       ;
    l_patch.patch_componants   := i_patch_componants ;
    l_patch.patch_created_by   := i_patch_created_by ;
    l_patch.note               := i_note             ;
    l_patch.rerunnable_yn      := i_rerunnable_yn    ;
 
    BEGIN
      l_patch.patch_create_date  := TO_DATE(i_patch_create_date,'MM-DD-YYYY');
    EXCEPTION
      WHEN OTHERS THEN
      l_patch.patch_create_date  := TO_DATE(i_patch_create_date,'DD-MON-YYYY');   
    END   ;
 
    l_patch.log_datetime       := SYSDATE;
    l_patch.completed_datetime := NULL;
    l_patch.success_YN         := 'N';
    l_patch.username           := USER;
    IF i_track_promotion = 'Y' THEN
      l_patch.retired_yn         := 'N';
    ELSE
      l_patch.retired_yn         := 'Y';
    END IF;
    
    dbms_output.enable(1000000);
    
    patches_tapi.ins_upd(io_patches => l_patch);
    g_patches := l_patch;

    IF i_remove_prereqs = 'Y' THEN
      delete from patch_prereqs where patch_name = l_patch.patch_name;
      dbms_output.put_line(SQL%ROWCOUNT||' patch_prereqs deleted');
    END IF;
    
    IF i_remove_sups = 'Y' THEN
      delete from patch_supersedes where patch_name = l_patch.patch_name;
      dbms_output.put_line(SQL%ROWCOUNT||' patch_supersedes deleted');
    END IF;

    COMMIT;
 
    put_lines('Starting patch '||l_patch.patch_name);
    put_lines(i_patch_desc);
    put_lines('Intended schema '||i_db_schema);
    put_lines('Created '||TO_CHAR(l_patch.patch_create_date,'DD-MON-YYYY'));
    
    IF USER <> UPPER(i_db_schema) THEN
      RAISE_APPLICATION_ERROR(-20000,'PATCH RUN AGAINST WRONG SCHEMA ('||USER||'). Intended for '||i_db_schema);   
    END IF;    
    
    put_lines('Log Start '||TO_CHAR(l_patch.log_datetime,G_DATETIME_MASK));
 
    put_lines('List objects already INVALID in this schema');
    list_invalid_schema_objects(i_schema => i_db_schema);    
 
  END;
 
  
  --------------------------------------------------------------
  -- list_invalid_schema_objects
  --------------------------------------------------------------
  PROCEDURE list_invalid_schema_objects(i_schema IN VARCHAR2 DEFAULT '%') IS
  BEGIN
 
    FOR l_dba_objects IN (select *
                          from dba_objects 
                          where (owner like UPPER(i_schema)) 
                          and status ='INVALID' ) LOOP
      put_lines('INVALID '||l_dba_objects.object_type||': '||l_dba_objects.owner ||'.' || l_dba_objects.OBJECT_NAME);  
 
    END LOOP;   

  END;
 
  --------------------------------------------------------------
  -- count_invalid_schema_objects
  --------------------------------------------------------------
  PROCEDURE count_invalid_schema_objects(i_schema IN VARCHAR2 DEFAULT '%') IS
  BEGIN
 
    FOR l_schema IN (select distinct owner, count(*) object_count 
                     from dba_objects 
                     where owner like i_schema  
                     and status ='INVALID' 
                     group by owner) LOOP
  
      put_lines('Schema '||l_schema.owner ||' ' ||  l_schema.object_count||' invalid objects found.');  
 
    END LOOP;

  END;
 
  --------------------------------------------------------------
  -- compile_schema
  --------------------------------------------------------------
  PROCEDURE compile_schema(i_schema IN VARCHAR2 DEFAULT '%') IS
  BEGIN
  
    dbms_output.enable(1000000);
  
    put_lines('Compiling schemas '||i_schema);
    
    count_invalid_schema_objects(i_schema => i_schema);
 
    --Recompile all objects in dependancy order.
    FOR l_object IN (        select max(level)  
                           , object_id 
                           , owner
                           , object_name     
                           , object_type     
                      from 
                      (select b.d_obj# object_id
                            , b.p_obj# referenced_object_id
                            , a.owner
                            , a.object_name
                            , a.object_type
                       from dba_objects       a
                           ,sys.dependency$ b
                       where a.object_id = b.d_obj# (+)
                       and   a.owner LIKE i_schema 
                       and object_name <> 'PATCH_INSTALLER'
                         and a.object_type in (
                           'FUNCTION'
                         , 'PROCEDURE'
                         , 'PACKAGE'
                         , 'PACKAGE BODY'
                         , 'TYPE'
                         , 'TYPE BODY'
                         , 'VIEW'
                         , 'MATERIALIZED VIEW'
                         , 'TRIGGER'
						 , 'SYNONYM') 
                      and a.status='INVALID')
                      connect by object_id = prior referenced_object_id
                      group by object_id 
                           , owner
                           , object_name     
                           , object_type
                      order by 1 DESC
                              ,DECODE(object_type,'PACKAGE',1,'TYPE',1,'PACKAGE BODY',2,'TYPE BODY',2, 'SYNONYM',3,0)) LOOP

  
     IF l_object.object_type = 'PACKAGE BODY' THEN
        do_sql_with_warning(i_sql => 'ALTER PACKAGE ' || l_object.owner||'.'||l_object.object_name || ' COMPILE BODY');
     ELSIF l_object.object_type = 'TYPE BODY' THEN
        do_sql_with_warning(i_sql => 'ALTER TYPE ' || l_object.owner||'.'||l_object.object_name || ' COMPILE BODY');
     ELSE
        do_sql_with_warning(i_sql => 'ALTER '||l_object.object_type||' ' || l_object.owner||'.'||l_object.object_name || ' COMPILE');      
     END IF;
 
    END LOOP;     
 
    put_lines('Remaining invalid objects in schema '||i_schema);
    list_invalid_schema_objects(i_schema => i_schema);
 
  END;
 
  --------------------------------------------------------------
  -- patch_log
  --------------------------------------------------------------
  --PROCEDURE patch_log(i_log                 IN VARCHAR2  );
 
  --------------------------------------------------------------
  -- patch_completed
  --------------------------------------------------------------
  PROCEDURE patch_completed(i_patch_name IN VARCHAR2 DEFAULT NULL) IS
                         
    l_patch patches%ROWTYPE;
    PRAGMA AUTONOMOUS_TRANSACTION;
    
  BEGIN
 
    IF i_patch_name IS NOT NULL THEN
      --Collection patches will be completed by patch_name
      g_patches := get_patches(i_patch_name => i_patch_name);
      
      if g_patches.patch_type in ('patchset','minor','major') then
        --For collection patches
        --find total errors and warnings for each componant patch
        SELECT sum(warning_count)  
              ,sum(error_count)   into g_warning_count, g_error_count
        FROM patches
        where g_patches.patch_componants like '%'||patch_name||'%';        
 
      end if;
      
    END IF;
 
    UPDATE patches
    SET  completed_datetime  = SYSDATE
        ,success_YN          = DECODE(g_error_count,0,'Y','N')
        ,warning_count       = g_warning_count
        ,error_count         = g_error_count
    WHERE patch_id = g_patches.patch_id;
 
    COMMIT;
    
    put_lines('Completed '||g_patches.patch_name||' at '||TO_CHAR(SYSDATE,G_DATETIME_MASK));
    put_lines('Warnings :'||g_warning_count);
    put_lines('Errors   :'||g_error_count);
    IF g_error_count <> 0 THEN
      put_lines('PATCH FAILED.');
 
    END IF;
    
    reset_patch;
 
  END;
 
  --------------------------------------------------------------
  -- get_dba_objects
  --------------------------------------------------------------
  FUNCTION get_dba_objects(i_owner       IN VARCHAR2
                          ,i_object_name IN VARCHAR2
                          ,i_object_type IN VARCHAR2) RETURN dba_objects%ROWTYPE IS
      CURSOR cu_dba_objects(c_owner       VARCHAR2
                           ,c_object_name VARCHAR2
                           ,c_object_type VARCHAR2) IS
      SELECT * 
      FROM dba_objects
      WHERE owner       = c_owner       
      AND   object_name = c_object_name
      AND   object_type = c_object_type;
  
      l_dba_objects dba_objects%ROWTYPE;
  
  BEGIN
    OPEN cu_dba_objects(c_owner       => i_owner      
                       ,c_object_name => i_object_name 
                       ,c_object_type => i_object_type);
    FETCH cu_dba_objects INTO l_dba_objects;
    CLOSE cu_dba_objects;
    
    RETURN l_dba_objects;
 
  END;
 
 
  --------------------------------------------------------------
  -- get_patches
  --------------------------------------------------------------
  FUNCTION get_patches(i_patch_name       IN VARCHAR2  ) RETURN patches%ROWTYPE IS
 
  BEGIN
 
    RETURN patches_tapi.patches_uk1(i_patch_name => i_patch_name);
    
  END;
 
  --------------------------------------------------------------
  -- add_patch_supersedes 
  --------------------------------------------------------------
  PROCEDURE add_patch_supersedes( i_patch_name       IN VARCHAR2
                                 ,i_supersedes_patch IN VARCHAR2 ) IS
    l_patch_supersedes patch_supersedes%ROWTYPE;
    PRAGMA AUTONOMOUS_TRANSACTION;
    
  BEGIN
    --If the superseding patch was successful, then record that a patch was superseded
    IF get_patches(i_patch_name   => i_patch_name).success_yn = 'Y' THEN
 
      l_patch_supersedes.patch_name         := i_patch_name          ;
      l_patch_supersedes.supersedes_patch   := i_supersedes_patch   ;
      
      patch_supersedes_tapi.ins_upd( io_patch_supersedes => l_patch_supersedes);
 
      COMMIT;
      
      put_lines('SUPERSEDES PATCH:'||i_supersedes_patch);
    
    END IF;
    
  EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
      NULL;    
 
  END add_patch_supersedes;
  
  --------------------------------------------------------------
  -- add_patch_prereq
  --------------------------------------------------------------
  PROCEDURE add_patch_prereq( i_patch_name   IN VARCHAR2
                             ,i_prereq_patch IN VARCHAR2 ) IS
    l_patch_prereq  patch_prereqs%ROWTYPE;
    PRAGMA AUTONOMOUS_TRANSACTION;
    
  BEGIN
    --If the prerequisite patch was successful, then record that a patch was superseded
    IF get_patches(i_patch_name   => i_prereq_patch ).success_yn = 'Y' THEN
 
      l_patch_prereq.patch_name     := i_patch_name      ;
      l_patch_prereq.prereq_patch   := i_prereq_patch    ;
 
      patch_prereqs_tapi.ins_upd( io_patch_prereqs => l_patch_prereq);
      
      COMMIT;
      
      put_lines('PREREQUISITE PATCH:'||i_prereq_patch);
      
    ELSE

      RAISE_APPLICATION_ERROR(-20000,'PREREQUISITE PATCH ['||i_prereq_patch||'] HAS NOT BEEN APPLIED!');       
    
    END IF;
    
  EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
      NULL;    
 
  END;
  
  
--------------------------------------------------------------------
--get_patch_dependency_tab
--returns patches_tab - patches in install order
-------------------------------------------------------------------- 

FUNCTION get_patch_dependency_tab RETURN patches_tab IS
 
  TYPE patch_set_tab   IS TABLE OF patches%ROWTYPE INDEX BY VARCHAR2(100);
 
  l_patch_stack   patches_tab;
  l_patch_set     patch_set_tab;
 
  CURSOR   cu_patches IS 
  select   p.*
  from     patches p
  where    success_yn = 'Y' 
  and      retired_yn = 'N' --exclude retired patches
  and      patch_name not in (select supersedes_patch from patch_supersedes) --exclude superseded patches
  order by completed_datetime;
  
  PROCEDURE stack_patch(i_patches         IN patches%ROWTYPE
                      , i_recursion_level IN INTEGER) IS
                      
    CURSOR cu_patch_prereqs(c_patch_name VARCHAR2) IS
    select p.* 
    from   patches p, 
           patch_prereqs pr
    where  p.patch_name = pr.prereq_patch 
    and    pr.patch_name = c_patch_name;  

    CURSOR cu_patch_superseding(c_patch_name VARCHAR2) IS
    select p.* 
    from   patches p, 
           patch_supersedes ps
    where  p.patch_name = ps.patch_name 
    and    ps.supersedes_patch = c_patch_name;  
	

     TYPE patch_tab_typ IS TABLE OF patches%ROWTYPE;
     l_patch_prereqs_tab    patch_tab_typ;
     l_patch_supersedes_tab patch_tab_typ;

                  
  BEGIN
    IF i_recursion_level > 1000 THEN
      RAISE_APPLICATION_ERROR(-20000,'Infinite Recursion detected');
    END IF;
    
    IF NOT l_patch_set.EXISTS(i_patches.patch_name) THEN
     

      OPEN cu_patch_prereqs(c_patch_name => i_patches.patch_name); 
      FETCH cu_patch_prereqs BULK COLLECT INTO l_patch_prereqs_tab;
      CLOSE cu_patch_prereqs;
 
      declare
        l_prereq_index BINARY_INTEGER;
      begin  
        --Loop thru the prereq patches.
        l_prereq_index := l_patch_prereqs_tab.first;
        WHILE l_prereq_index IS NOT NULL LOOP
  
          --Before we stack the prereq patch, lets check whether it has been superseded.
          declare
            l_superseded boolean := false;
          begin

/*
            OPEN cu_patch_superseding(c_patch_name => l_patch_prereqs_tab(l_prereq_index).patch_name); 
            FETCH cu_patch_superseding BULK COLLECT INTO l_patch_supersedes_tab;
            CLOSE cu_patch_superseding;
       
            declare
              l_supersede_index BINARY_INTEGER;
            begin  
              --Loop thru the prereq patches.
              l_supersede_index := l_patch_supersedes_tab.first;
              WHILE l_supersede_index IS NOT NULL LOOP
 
                --Stack superseding patches instead...
                l_superseded := true;
                RAISE_APPLICATION_ERROR(-20000,'Supersede detected');
                  stack_patch(i_patches         => l_patch_supersedes_tab(l_supersede_index)
                            , i_recursion_level => i_recursion_level + 1);
                  

                l_supersede_index := l_patch_supersedes_tab.next(l_supersede_index);             
              END LOOP;
            end;
       */
            --exclude superseded from the dependancies too! (but keep retired patches)
            if not l_superseded then
              --stack the prereq patch
                  stack_patch(i_patches         => l_patch_prereqs_tab(l_prereq_index)
                            , i_recursion_level => i_recursion_level + 1);
            end if;
          end;
    
          l_prereq_index := l_patch_prereqs_tab.next(l_prereq_index);
        END LOOP;
      end;
 
 
      l_patch_stack.EXTEND;
      l_patch_stack(l_patch_stack.LAST) := i_patches;
      l_patch_set(i_patches.patch_name) := i_patches;
 
    END IF;
 
  END stack_patch;  
 
BEGIN
 
  --initialise the stack.
  l_patch_stack := patches_tab();
 
  FOR l_patch IN cu_patches LOOP
  
    stack_patch(i_patches         => l_patch
              , i_recursion_level => 1);
      
  END LOOP;
  
  return l_patch_stack;

END;    
  

--------------------------------------------------------------------
--patch_dependency_tab
--returns PIPELINED patches_tab - patches in install order
-------------------------------------------------------------------- 

FUNCTION patch_dependency_tab RETURN patches_tab PIPELINED IS

  l_index BINARY_INTEGER := 0; 
  l_patch_stack   patches_tab;
 
BEGIN
 
  l_patch_stack := get_patch_dependency_tab;
  
  --Now pipe the tab 
  l_index := l_patch_stack.FIRST;
  WHILE l_index IS NOT NULL LOOP
    PIPE ROW(l_patch_stack(l_index));
    l_index := l_patch_stack.NEXT(l_index);
  END LOOP;
 
END;  


--------------------------------------------------------------------
--patch_dependency
--returns dbms_output list of patches in install order.
-------------------------------------------------------------------- 

PROCEDURE patch_dependency IS
  
  l_index BINARY_INTEGER := 0; 
  l_patch_stack   patches_tab;
 
BEGIN
  dbms_output.enable(1000000);
  l_patch_stack := get_patch_dependency_tab;
  
  --Now pipe the tab 
  l_index := l_patch_stack.FIRST;
  WHILE l_index IS NOT NULL LOOP
    dbms_output.put_line(l_patch_stack(l_index).patch_name);
    l_index := l_patch_stack.NEXT(l_index);
  END LOOP;
 
END;  
 
 
--------------------------------------------------------------------
--get_patch_component_tab
--returns patch_components_tab - patches as components
-------------------------------------------------------------------- 

FUNCTION get_patch_component_tab(i_patch_name IN VARCHAR2) RETURN patch_components_tab IS
 
  l_patch_list   patch_components_tab;
 
  l_patches patches%ROWTYPE;
  
  l_patch_components           VARCHAR2(32000);
  l_patch_component_path       VARCHAR2(300);
  l_patch_component_patch_name VARCHAR2(100);
 
BEGIN

  l_patches := get_patches(i_patch_name => i_patch_name );
  
  l_patch_components := l_patches.patch_componants;
  
  
  --initialise the stack.
  l_patch_list := patch_components_tab();
  
  WHILE l_patch_components IS NOT NULL LOOP
    l_patch_component_path := text_manip.f_remove_first_element(io_list => l_patch_components
                                                               ,i_delim => ',');
                                               
    l_patch_component_patch_name := text_manip.f_get_last_element(i_list   => l_patch_component_path  
                                                                 ,i_delim  => '\ '); 
 
    l_patch_list.EXTEND;
    l_patch_list(l_patch_list.LAST)   := l_patch_component_patch_name;
 
  END LOOP;
 
  return l_patch_list;

END;     

--------------------------------------------------------------------
--patch_component_tab
--returns PIPELINED patch_components_tab - patches as components
-------------------------------------------------------------------- 

FUNCTION patch_component_tab(i_patch_name IN VARCHAR2) RETURN patch_components_tab PIPELINED IS

  l_index BINARY_INTEGER := 0; 
  l_patch_list   patch_components_tab;
 
BEGIN
 
  l_patch_list := get_patch_component_tab(i_patch_name => i_patch_name);
  
  --Now pipe the tab 
  l_index := l_patch_list.FIRST;
  WHILE l_index IS NOT NULL LOOP
    PIPE ROW(l_patch_list(l_index));
    l_index := l_patch_list.NEXT(l_index);
  END LOOP;
 
END;  


--------------------------------------------------------------------
--patch_component
--returns dbms_output list of - patches as components
-------------------------------------------------------------------- 

PROCEDURE patch_component(i_patch_name IN VARCHAR2) IS
  
  l_index BINARY_INTEGER := 0; 
  l_patch_list   patch_components_tab;
 
BEGIN
  dbms_output.enable(1000000);
  l_patch_list := get_patch_component_tab(i_patch_name => i_patch_name);
  
  --Now pipe the tab 
  l_index := l_patch_list.FIRST;
  WHILE l_index IS NOT NULL LOOP
    dbms_output.put_line(l_patch_list(l_index));
    l_index := l_patch_list.NEXT(l_index);
  END LOOP;
 
END;  

--------------------------------------------------------------------
--get_last_patch
--returns the patch_name of the last patch that installed the patch_component
-------------------------------------------------------------------- 

FUNCTION get_last_patch(i_patch_component IN VARCHAR2) RETURN VARCHAR2 IS

  CURSOR cu_last_patch(c_patch_component IN VARCHAR2) IS
  SELECT patch_name
  FROM   patches_components_v
  WHERE  patch_component = c_patch_component
  AND    success_yn = 'Y'
  ORDER BY completed_datetime DESC;

  l_result VARCHAR2(200);
  
BEGIN
  OPEN cu_last_patch(c_patch_component => i_patch_component);
  FETCH cu_last_patch INTO l_result;
  CLOSE cu_last_patch;
  
  RETURN l_result;
 
END;
 
END;
/
show error;
