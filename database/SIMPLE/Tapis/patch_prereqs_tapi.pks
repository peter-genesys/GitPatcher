CREATE OR REPLACE PACKAGE patch_prereqs_tapi IS                                                                                                       
                                                                                                                                                      
----------------------------------------------------------------                                                                                      
-- Used by COLLECTION FUNCTIONS                                                                                                                       
----------------------------------------------------------------                                                                                      
                                                                                                                                                      
TYPE patch_prereqs_tab IS TABLE OF patch_prereqs%ROWTYPE                                                                                              
   INDEX BY BINARY_INTEGER;                                                                                                                           
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- RECORD FUNCTIONS                                                                                                                                   
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereqs_rid - one row from rowid                                                                                                             
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereqs_rid (                                                                                                                          
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs%ROWTYPE RESULT_CACHE;                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereqs_pk - one row from primary key                                                                                                        
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereqs_pk (                                                                                                                           
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs%ROWTYPE RESULT_CACHE;                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereqs_uk1 - one row from unique index                                                                                                      
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereqs_uk1 (                                                                                                                          
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs%ROWTYPE RESULT_CACHE;                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- COLUMN FUNCTIONS                                                                                                                                   
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- get_rowid - rowid from primary key                                                                                                                 
-----------------------------------------------------------------                                                                                     
FUNCTION get_rowid (                                                                                                                                  
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN rowid RESULT_CACHE;                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- get_rowid - rowid from unique index patch_prereqs_uk1                                                                                              
-----------------------------------------------------------------                                                                                     
FUNCTION get_rowid (                                                                                                                                  
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN rowid RESULT_CACHE;                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereq_id - retrieved via rowid                                                                                                              
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereq_id (                                                                                                                            
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_prereq_id%TYPE;                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereq_id - retrieved via primary key patch_prereqs_pk                                                                                       
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereq_id (                                                                                                                            
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_prereq_id%TYPE;                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereq_id - retrieved via unique index patch_prereqs_uk1                                                                                     
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereq_id (                                                                                                                            
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_prereq_id%TYPE;                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_name - retrieved via rowid                                                                                                                   
-----------------------------------------------------------------                                                                                     
FUNCTION patch_name (                                                                                                                                 
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_name%TYPE;                                                                                                           
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_name - retrieved via primary key patch_prereqs_pk                                                                                            
-----------------------------------------------------------------                                                                                     
FUNCTION patch_name (                                                                                                                                 
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_name%TYPE;                                                                                                           
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_name - retrieved via unique index patch_prereqs_uk1                                                                                          
-----------------------------------------------------------------                                                                                     
FUNCTION patch_name (                                                                                                                                 
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_name%TYPE;                                                                                                           
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- prereq_patch - retrieved via rowid                                                                                                                 
-----------------------------------------------------------------                                                                                     
FUNCTION prereq_patch (                                                                                                                               
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.prereq_patch%TYPE;                                                                                                         
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- prereq_patch - retrieved via primary key patch_prereqs_pk                                                                                          
-----------------------------------------------------------------                                                                                     
FUNCTION prereq_patch (                                                                                                                               
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.prereq_patch%TYPE;                                                                                                         
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- prereq_patch - retrieved via unique index patch_prereqs_uk1                                                                                        
-----------------------------------------------------------------                                                                                     
FUNCTION prereq_patch (                                                                                                                               
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.prereq_patch%TYPE;                                                                                                         
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_by - retrieved via rowid                                                                                                                   
-----------------------------------------------------------------                                                                                     
FUNCTION created_by (                                                                                                                                 
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_by%TYPE;                                                                                                           
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_by - retrieved via primary key patch_prereqs_pk                                                                                            
-----------------------------------------------------------------                                                                                     
FUNCTION created_by (                                                                                                                                 
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_by%TYPE;                                                                                                           
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_by - retrieved via unique index patch_prereqs_uk1                                                                                          
-----------------------------------------------------------------                                                                                     
FUNCTION created_by (                                                                                                                                 
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_by%TYPE;                                                                                                           
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_on - retrieved via rowid                                                                                                                   
-----------------------------------------------------------------                                                                                     
FUNCTION created_on (                                                                                                                                 
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_on%TYPE;                                                                                                           
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_on - retrieved via primary key patch_prereqs_pk                                                                                            
-----------------------------------------------------------------                                                                                     
FUNCTION created_on (                                                                                                                                 
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_on%TYPE;                                                                                                           
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_on - retrieved via unique index patch_prereqs_uk1                                                                                          
-----------------------------------------------------------------                                                                                     
FUNCTION created_on (                                                                                                                                 
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_on%TYPE;                                                                                                           
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_by - retrieved via rowid                                                                                                              
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_by (                                                                                                                            
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_by%TYPE;                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_by - retrieved via primary key patch_prereqs_pk                                                                                       
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_by (                                                                                                                            
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_by%TYPE;                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_by - retrieved via unique index patch_prereqs_uk1                                                                                     
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_by (                                                                                                                            
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_by%TYPE;                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_on - retrieved via rowid                                                                                                              
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_on (                                                                                                                            
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_on%TYPE;                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_on - retrieved via primary key patch_prereqs_pk                                                                                       
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_on (                                                                                                                            
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_on%TYPE;                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_on - retrieved via unique index patch_prereqs_uk1                                                                                     
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_on (                                                                                                                            
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_on%TYPE;                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- get_current_rec                                                                                                                                    
-----------------------------------------------------------------                                                                                     
-- get the current record by pk if given, otherwise by uk1                                                                                            
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
FUNCTION get_current_rec(                                                                                                                             
             i_patch_prereqs IN patch_prereqs%rowtype                                                                                                 
            ,i_raise_error IN VARCHAR2 DEFAULT 'Y'                                                                                                    
) RETURN patch_prereqs%rowtype;                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- create_rec                                                                                                                                         
-----------------------------------------------------------------                                                                                     
-- create a record from its component fields                                                                                                          
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
FUNCTION create_rec(                                                                                                                                  
     i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE DEFAULT NULL                                                                             
    ,i_patch_name      IN patch_prereqs.patch_name     %TYPE DEFAULT NULL                                                                             
    ,i_prereq_patch    IN patch_prereqs.prereq_patch   %TYPE DEFAULT NULL                                                                             
    ,i_created_by      IN patch_prereqs.created_by     %TYPE DEFAULT NULL                                                                             
    ,i_created_on      IN patch_prereqs.created_on     %TYPE DEFAULT NULL                                                                             
    ,i_last_updated_by IN patch_prereqs.last_updated_by%TYPE DEFAULT NULL                                                                             
    ,i_last_updated_on IN patch_prereqs.last_updated_on%TYPE DEFAULT NULL                                                                             
) RETURN patch_prereqs%ROWTYPE;                                                                                                                       
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- split_rec                                                                                                                                          
-----------------------------------------------------------------                                                                                     
-- split a record into its component fields                                                                                                           
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE split_rec( i_patch_prereqs in patch_prereqs%rowtype                                                                                         
                    ,o_patch_prereq_id OUT patch_prereqs.patch_prereq_id%TYPE                                                                         
                    ,o_patch_name      OUT patch_prereqs.patch_name     %TYPE                                                                         
                    ,o_prereq_patch    OUT patch_prereqs.prereq_patch   %TYPE                                                                         
                    ,o_created_by      OUT patch_prereqs.created_by     %TYPE                                                                         
                    ,o_created_on      OUT patch_prereqs.created_on     %TYPE                                                                         
                    ,o_last_updated_by OUT patch_prereqs.last_updated_by%TYPE                                                                         
                    ,o_last_updated_on OUT patch_prereqs.last_updated_on%TYPE                                                                         
);                                                                                                                                                    
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- merge_old_and_new                                                                                                                                  
-----------------------------------------------------------------                                                                                     
-- null values in NEW replaced with values from OLD                                                                                                   
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE merge_old_and_new(i_old_rec  IN     patch_prereqs%rowtype                                                                                   
                           ,io_new_rec IN OUT patch_prereqs%rowtype);                                                                                 
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- INSERT                                                                                                                                             
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- ins                                                                                                                                                
-----------------------------------------------------------------                                                                                     
-- insert a record - using record type, returning record                                                                                              
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
                                                                                                                                                      
PROCEDURE ins(                                                                                                                                        
    io_patch_prereqs  in out patch_prereqs%rowtype );                                                                                                 
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- ins                                                                                                                                                
-----------------------------------------------------------------                                                                                     
-- insert a record - using components, returning components                                                                                           
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
                                                                                                                                                      
PROCEDURE ins(                                                                                                                                        
     io_patch_prereq_id IN OUT patch_prereqs.patch_prereq_id%TYPE                                                                                     
    ,io_patch_name      IN OUT patch_prereqs.patch_name%TYPE                                                                                          
    ,io_prereq_patch    IN OUT patch_prereqs.prereq_patch%TYPE                                                                                        
    ,io_created_by      IN OUT patch_prereqs.created_by%TYPE                                                                                          
    ,io_created_on      IN OUT patch_prereqs.created_on%TYPE                                                                                          
    ,io_last_updated_by IN OUT patch_prereqs.last_updated_by%TYPE                                                                                     
    ,io_last_updated_on IN OUT patch_prereqs.last_updated_on%TYPE                                                                                     
);                                                                                                                                                    
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- ins_opt                                                                                                                                            
-----------------------------------------------------------------                                                                                     
-- insert a record - using components, all optional                                                                                                   
-----------------------------------------------------------------                                                                                     
PROCEDURE ins_opt(                                                                                                                                    
     i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE DEFAULT NULL                                                                             
    ,i_patch_name      IN patch_prereqs.patch_name     %TYPE DEFAULT NULL                                                                             
    ,i_prereq_patch    IN patch_prereqs.prereq_patch   %TYPE DEFAULT NULL                                                                             
    ,i_created_by      IN patch_prereqs.created_by     %TYPE DEFAULT NULL                                                                             
    ,i_created_on      IN patch_prereqs.created_on     %TYPE DEFAULT NULL                                                                             
    ,i_last_updated_by IN patch_prereqs.last_updated_by%TYPE DEFAULT NULL                                                                             
    ,i_last_updated_on IN patch_prereqs.last_updated_on%TYPE DEFAULT NULL                                                                             
);                                                                                                                                                    
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- UPDATE                                                                                                                                             
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- upd                                                                                                                                                
-----------------------------------------------------------------                                                                                     
-- update a record - using record type, returning record                                                                                              
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
                                                                                                                                                      
PROCEDURE upd(                                                                                                                                        
    io_patch_prereqs  in out patch_prereqs%rowtype );                                                                                                 
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- upd                                                                                                                                                
-----------------------------------------------------------------                                                                                     
-- update a record - using components, returning components                                                                                           
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE upd(                                                                                                                                        
     io_patch_prereq_id IN OUT patch_prereqs.patch_prereq_id%TYPE                                                                                     
    ,io_patch_name      IN OUT patch_prereqs.patch_name%TYPE                                                                                          
    ,io_prereq_patch    IN OUT patch_prereqs.prereq_patch%TYPE                                                                                        
    ,io_created_by      IN OUT patch_prereqs.created_by%TYPE                                                                                          
    ,io_created_on      IN OUT patch_prereqs.created_on%TYPE                                                                                          
    ,io_last_updated_by IN OUT patch_prereqs.last_updated_by%TYPE                                                                                     
    ,io_last_updated_on IN OUT patch_prereqs.last_updated_on%TYPE                                                                                     
);                                                                                                                                                    
                                                                                                                                                      
-------------------------------------------------------------------------                                                                             
-- upd_not_null                                                                                                                                       
-------------------------------------------------------------------------                                                                             
-- update a record                                                                                                                                    
--   using components, all optional                                                                                                                   
--   by pk if given, otherwise by uk1, null values ignored.                                                                                           
-----------------------------------------------------------------                                                                                     
PROCEDURE upd_not_null(                                                                                                                               
     i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE DEFAULT NULL                                                                             
    ,i_patch_name      IN patch_prereqs.patch_name%TYPE DEFAULT NULL                                                                                  
    ,i_prereq_patch    IN patch_prereqs.prereq_patch%TYPE DEFAULT NULL                                                                                
    ,i_created_by      IN patch_prereqs.created_by%TYPE DEFAULT NULL                                                                                  
    ,i_created_on      IN patch_prereqs.created_on%TYPE DEFAULT NULL                                                                                  
    ,i_last_updated_by IN patch_prereqs.last_updated_by%TYPE DEFAULT NULL                                                                             
    ,i_last_updated_on IN patch_prereqs.last_updated_on%TYPE DEFAULT NULL                                                                             
);                                                                                                                                                    
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- upd_patch_prereqs_uk1 - use uk to update itself                                                                                                    
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE upd_patch_prereqs_uk1 (                                                                                                                     
     i_old_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                              
    ,i_old_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                            
      ,i_new_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                            
                                                                                                                                                      
    ,i_new_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                            
                                                                                                                                                      
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   );                                                                                                                                                 
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- DELETE                                                                                                                                             
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- del                                                                                                                                                
-----------------------------------------------------------------                                                                                     
-- delete a record - using record type                                                                                                                
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
                                                                                                                                                      
PROCEDURE del(                                                                                                                                        
    i_patch_prereqs  in patch_prereqs%rowtype );                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- del                                                                                                                                                
-----------------------------------------------------------------                                                                                     
-- delete a record                                                                                                                                    
--   using components, all optional                                                                                                                   
--   by pk if given, otherwise by uk1                                                                                                                 
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE del(                                                                                                                                        
     i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE DEFAULT NULL                                                                             
    ,i_patch_name      IN patch_prereqs.patch_name%TYPE DEFAULT NULL                                                                                  
    ,i_prereq_patch    IN patch_prereqs.prereq_patch%TYPE DEFAULT NULL                                                                                
    ,i_created_by      IN patch_prereqs.created_by%TYPE DEFAULT NULL                                                                                  
    ,i_created_on      IN patch_prereqs.created_on%TYPE DEFAULT NULL                                                                                  
    ,i_last_updated_by IN patch_prereqs.last_updated_by%TYPE DEFAULT NULL                                                                             
    ,i_last_updated_on IN patch_prereqs.last_updated_on%TYPE DEFAULT NULL                                                                             
    ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                            
);                                                                                                                                                    
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- INSERT OR UPDATE                                                                                                                                   
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- ins_upd                                                                                                                                            
-----------------------------------------------------------------                                                                                     
-- insert or update a record using record type, returning record                                                                                      
-- insert a record - if possible                                                                                                                      
-- update a record - by pk if given, otherwise by uk1                                                                                                 
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
                                                                                                                                                      
PROCEDURE ins_upd(                                                                                                                                    
    io_patch_prereqs  in out patch_prereqs%rowtype );                                                                                                 
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- ins_upd                                                                                                                                            
-----------------------------------------------------------------                                                                                     
-- insert or update a record using components, returning components                                                                                   
-- insert a record - if possible                                                                                                                      
-- update a record - by pk if given, otherwise by uk1                                                                                                 
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE ins_upd(                                                                                                                                    
     io_patch_prereq_id IN OUT patch_prereqs.patch_prereq_id%TYPE                                                                                     
    ,io_patch_name      IN OUT patch_prereqs.patch_name%TYPE                                                                                          
    ,io_prereq_patch    IN OUT patch_prereqs.prereq_patch%TYPE                                                                                        
    ,io_created_by      IN OUT patch_prereqs.created_by%TYPE                                                                                          
    ,io_created_on      IN OUT patch_prereqs.created_on%TYPE                                                                                          
    ,io_last_updated_by IN OUT patch_prereqs.last_updated_by%TYPE                                                                                     
    ,io_last_updated_on IN OUT patch_prereqs.last_updated_on%TYPE                                                                                     
);                                                                                                                                                    
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- ins_upd_not_null                                                                                                                                   
-----------------------------------------------------------------                                                                                     
-- insert or update a record using components, all optional                                                                                           
-- insert a record - if possible                                                                                                                      
-- update a record - by pk if given, otherwise by uk1, null values ignored.                                                                           
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE ins_upd_not_null(                                                                                                                           
     i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE DEFAULT NULL                                                                             
    ,i_patch_name      IN patch_prereqs.patch_name%TYPE DEFAULT NULL                                                                                  
    ,i_prereq_patch    IN patch_prereqs.prereq_patch%TYPE DEFAULT NULL                                                                                
    ,i_created_by      IN patch_prereqs.created_by%TYPE DEFAULT NULL                                                                                  
    ,i_created_on      IN patch_prereqs.created_on%TYPE DEFAULT NULL                                                                                  
    ,i_last_updated_by IN patch_prereqs.last_updated_by%TYPE DEFAULT NULL                                                                             
    ,i_last_updated_on IN patch_prereqs.last_updated_on%TYPE DEFAULT NULL                                                                             
);                                                                                                                                                    
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- DATA UNLOADING                                                                                                                                     
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- unload_data                                                                                                                                        
------------------------------------------------------------------------------                                                                        
-- unload data into a script ins_upd statements                                                                                                       
------------------------------------------------------------------------------                                                                        
procedure unload_data;                                                                                                                                
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- COLLECTION FUNCTIONS                                                                                                                               
------------------------------------------------------------------------------                                                                        
-----------------------------------------------------------------                                                                                     
-- collections_equal                                                                                                                                  
-----------------------------------------------------------------                                                                                     
-- Return true if the the contents of the two collections are the same.                                                                               
-- In this variant, the collection is based on the rowtype of the                                                                                     
-- the table patch_prereqs,                                                                                                                           
-- i_collection1    - first collection for comparison                                                                                                 
-- i_collection2    - second collection for comparison                                                                                                
-- i_match_indexes  - if TRUE, then the row numbers in the two                                                                                        
--                         collections must also match.                                                                                               
-- i_both_null_true - if TRUE, then if values in corresponding rows                                                                                   
--                         of both collections are NULL, treat this as equality.                                                                      
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
FUNCTION collections_equal (                                                                                                                          
  i_collection1     IN   patch_prereqs_tab                                                                                                            
, i_collection2     IN   patch_prereqs_tab                                                                                                            
, i_match_indexes   IN   BOOLEAN DEFAULT TRUE                                                                                                         
, i_both_null_true  IN   BOOLEAN DEFAULT TRUE                                                                                                         
)                                                                                                                                                     
RETURN BOOLEAN;                                                                                                                                       
                                                                                                                                                      
END patch_prereqs_tapi;                                                                                                                               
/
show error;
