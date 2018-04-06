CREATE OR REPLACE PACKAGE BODY patch_prereqs_tapi IS                                                                                                  
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- RECORD FUNCTIONS                                                                                                                                   
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereqs_rid - one row from rowid                                                                                                             
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereqs_rid (                                                                                                                          
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs%ROWTYPE RESULT_CACHE RELIES_ON (patch_prereqs) IS                                                                          
                                                                                                                                                      
   CURSOR cr_patch_prereqs IS                                                                                                                         
      SELECT *                                                                                                                                        
        FROM patch_prereqs                                                                                                                            
       WHERE rowid = i_rowid;                                                                                                                         
                                                                                                                                                      
   l_result patch_prereqs%ROWTYPE;                                                                                                                    
   l_found   BOOLEAN;                                                                                                                                 
   x_unknown_rowid EXCEPTION;                                                                                                                         
BEGIN                                                                                                                                                 
   OPEN cr_patch_prereqs;                                                                                                                             
   FETCH cr_patch_prereqs INTO l_result;                                                                                                              
   l_found := cr_patch_prereqs%FOUND;                                                                                                                 
   CLOSE cr_patch_prereqs;                                                                                                                            
                                                                                                                                                      
   IF NOT l_found AND                                                                                                                                 
      i_raise_error = 'Y' THEN                                                                                                                        
     RAISE x_unknown_rowid;                                                                                                                           
   END IF;                                                                                                                                            
                                                                                                                                                      
   RETURN l_result;                                                                                                                                   
                                                                                                                                                      
EXCEPTION                                                                                                                                             
  WHEN x_unknown_rowid THEN                                                                                                                           
--    add_log('patch_prereqs_rid: Unknown rowid value'||                                                                                              
--[FOREACH]pkycol[between]||                                                                                                                          
--    ' i_[colname/l]='||i_[colname/l]                                                                                                                
--[ENDFOREACH]                                                                                                                                        
--     );                                                                                                                                             
    RAISE NO_DATA_FOUND;                                                                                                                              
                                                                                                                                                      
END patch_prereqs_rid;                                                                                                                                
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereqs_pk - one row from primary key                                                                                                        
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereqs_pk (                                                                                                                           
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs%ROWTYPE RESULT_CACHE RELIES_ON (patch_prereqs) IS                                                                          
                                                                                                                                                      
   CURSOR cr_patch_prereqs IS                                                                                                                         
      SELECT *                                                                                                                                        
        FROM patch_prereqs                                                                                                                            
       WHERE patch_prereq_id = i_patch_prereq_id;                                                                                                     
                                                                                                                                                      
   l_result patch_prereqs%ROWTYPE;                                                                                                                    
   l_found   BOOLEAN;                                                                                                                                 
   x_unknown_key EXCEPTION;                                                                                                                           
BEGIN                                                                                                                                                 
   OPEN cr_patch_prereqs;                                                                                                                             
   FETCH cr_patch_prereqs INTO l_result;                                                                                                              
   l_found := cr_patch_prereqs%FOUND;                                                                                                                 
   CLOSE cr_patch_prereqs;                                                                                                                            
                                                                                                                                                      
   IF NOT l_found AND                                                                                                                                 
      i_raise_error = 'Y' THEN                                                                                                                        
     RAISE x_unknown_key;                                                                                                                             
   END IF;                                                                                                                                            
                                                                                                                                                      
   RETURN l_result;                                                                                                                                   
                                                                                                                                                      
EXCEPTION                                                                                                                                             
  WHEN x_unknown_key THEN                                                                                                                             
--    add_log('patch_prereqs_pk: Unknown key value'||                                                                                                 
--[FOREACH]pkycol[between]||                                                                                                                          
--    ' i_[colname/l]='||i_[colname/l]                                                                                                                
--[ENDFOREACH]                                                                                                                                        
--     );                                                                                                                                             
    RAISE NO_DATA_FOUND;                                                                                                                              
                                                                                                                                                      
END patch_prereqs_pk;                                                                                                                                 
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereqs_uk1 - one row from unique index                                                                                                      
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereqs_uk1 (                                                                                                                          
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs%ROWTYPE RESULT_CACHE RELIES_ON (patch_prereqs) IS                                                                          
                                                                                                                                                      
   CURSOR cr_patch_prereqs IS                                                                                                                         
      SELECT *                                                                                                                                        
        FROM patch_prereqs                                                                                                                            
       WHERE ((i_patch_name is null and patch_name is null) or patch_name = i_patch_name  )                                                           
         AND ((i_prereq_patch is null and prereq_patch is null) or prereq_patch = i_prereq_patch);                                                    
                                                                                                                                                      
   l_result patch_prereqs%ROWTYPE;                                                                                                                    
   l_found   BOOLEAN;                                                                                                                                 
   x_unknown_key EXCEPTION;                                                                                                                           
BEGIN                                                                                                                                                 
   OPEN cr_patch_prereqs;                                                                                                                             
   FETCH cr_patch_prereqs INTO l_result;                                                                                                              
   l_found := cr_patch_prereqs%FOUND;                                                                                                                 
   CLOSE cr_patch_prereqs;                                                                                                                            
                                                                                                                                                      
   IF NOT l_found AND                                                                                                                                 
      i_raise_error = 'Y' THEN                                                                                                                        
     RAISE x_unknown_key;                                                                                                                             
   END IF;                                                                                                                                            
                                                                                                                                                      
   RETURN l_result;                                                                                                                                   
                                                                                                                                                      
EXCEPTION                                                                                                                                             
  WHEN x_unknown_key THEN                                                                                                                             
--    add_log('[index_name/l]: Unknown key value'||                                                                                                   
--[FOREACH]uindcol[between]||                                                                                                                         
--    ' i_[colname/l]='||i_[colname/l]                                                                                                                
--[ENDFOREACH]                                                                                                                                        
--     );                                                                                                                                             
    RAISE NO_DATA_FOUND;                                                                                                                              
                                                                                                                                                      
END patch_prereqs_uk1;                                                                                                                                
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- COLUMN FUNCTIONS                                                                                                                                   
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- get_rowid - rowid from primary key                                                                                                                 
-----------------------------------------------------------------                                                                                     
FUNCTION get_rowid (                                                                                                                                  
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN rowid RESULT_CACHE RELIES_ON (patch_prereqs) IS                                                                                          
                                                                                                                                                      
   CURSOR cr_patch_prereqs IS                                                                                                                         
      SELECT rowid                                                                                                                                    
        FROM patch_prereqs                                                                                                                            
       WHERE patch_prereq_id = i_patch_prereq_id;                                                                                                     
                                                                                                                                                      
   l_result rowid;                                                                                                                                    
   l_found   BOOLEAN;                                                                                                                                 
   x_unknown_key EXCEPTION;                                                                                                                           
BEGIN                                                                                                                                                 
   OPEN cr_patch_prereqs;                                                                                                                             
   FETCH cr_patch_prereqs INTO l_result;                                                                                                              
   l_found := cr_patch_prereqs%FOUND;                                                                                                                 
   CLOSE cr_patch_prereqs;                                                                                                                            
                                                                                                                                                      
   IF NOT l_found AND                                                                                                                                 
      i_raise_error = 'Y' THEN                                                                                                                        
     RAISE x_unknown_key;                                                                                                                             
   END IF;                                                                                                                                            
                                                                                                                                                      
   RETURN l_result;                                                                                                                                   
                                                                                                                                                      
EXCEPTION                                                                                                                                             
  WHEN x_unknown_key THEN                                                                                                                             
--    add_log('patch_prereqs_pk: Unknown key value'||                                                                                                 
--[FOREACH]pkycol[between]||                                                                                                                          
--    ' i_[colname/l]='||i_[colname/l]                                                                                                                
--[ENDFOREACH]                                                                                                                                        
--     );                                                                                                                                             
    RAISE NO_DATA_FOUND;                                                                                                                              
                                                                                                                                                      
END get_rowid;                                                                                                                                        
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- get_rowid - rowid from unique index patch_prereqs_uk1                                                                                              
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereqs_uk1 (                                                                                                                          
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN rowid RESULT_CACHE RELIES_ON (patch_prereqs) IS                                                                                          
                                                                                                                                                      
   CURSOR cr_patch_prereqs IS                                                                                                                         
      SELECT rowid                                                                                                                                    
        FROM patch_prereqs                                                                                                                            
       WHERE ((i_patch_name is null and patch_name is null) or patch_name = i_patch_name  )                                                           
         AND ((i_prereq_patch is null and prereq_patch is null) or prereq_patch = i_prereq_patch);                                                    
                                                                                                                                                      
   l_result  rowid;                                                                                                                                   
   l_found   BOOLEAN;                                                                                                                                 
   x_unknown_key EXCEPTION;                                                                                                                           
BEGIN                                                                                                                                                 
   OPEN cr_patch_prereqs;                                                                                                                             
   FETCH cr_patch_prereqs INTO l_result;                                                                                                              
   l_found := cr_patch_prereqs%FOUND;                                                                                                                 
   CLOSE cr_patch_prereqs;                                                                                                                            
                                                                                                                                                      
   IF NOT l_found AND                                                                                                                                 
      i_raise_error = 'Y' THEN                                                                                                                        
     RAISE x_unknown_key;                                                                                                                             
   END IF;                                                                                                                                            
                                                                                                                                                      
   RETURN l_result;                                                                                                                                   
                                                                                                                                                      
EXCEPTION                                                                                                                                             
  WHEN x_unknown_key THEN                                                                                                                             
--    add_log('[index_name/l]: Unknown key value'||                                                                                                   
--[FOREACH]uindcol[between]||                                                                                                                         
--    ' i_[colname/l]='||i_[colname/l]                                                                                                                
--[ENDFOREACH]                                                                                                                                        
--     );                                                                                                                                             
    RAISE NO_DATA_FOUND;                                                                                                                              
                                                                                                                                                      
END get_rowid;                                                                                                                                        
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereq_id - retrieved via rowid                                                                                                              
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereq_id (                                                                                                                            
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_prereq_id%TYPE                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_rid (                                                                                                                          
   i_rowid => i_rowid                                                                                                                                 
  ,i_raise_error => i_raise_error                                                                                                                     
   ).patch_prereq_id;                                                                                                                                 
                                                                                                                                                      
END patch_prereq_id;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereq_id - retrieved via primary key patch_prereqs_pk                                                                                       
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereq_id (                                                                                                                            
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_prereq_id%TYPE                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_pk (                                                                                                                           
   i_patch_prereq_id => i_patch_prereq_id                                                                                                             
  ,i_raise_error => i_raise_error                                                                                                                     
   ).patch_prereq_id;                                                                                                                                 
                                                                                                                                                      
END patch_prereq_id;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_prereq_id - retrieved via unique index patch_prereqs_uk1                                                                                     
-----------------------------------------------------------------                                                                                     
FUNCTION patch_prereq_id (                                                                                                                            
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_prereq_id%TYPE                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_uk1(                                                                                                                           
   i_patch_name   => i_patch_name                                                                                                                     
  ,i_prereq_patch => i_prereq_patch                                                                                                                   
,i_raise_error => i_raise_error                                                                                                                       
   ).patch_prereq_id;                                                                                                                                 
                                                                                                                                                      
END patch_prereq_id;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_name - retrieved via rowid                                                                                                                   
-----------------------------------------------------------------                                                                                     
FUNCTION patch_name (                                                                                                                                 
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_name%TYPE                                                                                                            
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_rid (                                                                                                                          
   i_rowid => i_rowid                                                                                                                                 
  ,i_raise_error => i_raise_error                                                                                                                     
   ).patch_name;                                                                                                                                      
                                                                                                                                                      
END patch_name;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_name - retrieved via primary key patch_prereqs_pk                                                                                            
-----------------------------------------------------------------                                                                                     
FUNCTION patch_name (                                                                                                                                 
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_name%TYPE                                                                                                            
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_pk (                                                                                                                           
   i_patch_prereq_id => i_patch_prereq_id                                                                                                             
  ,i_raise_error => i_raise_error                                                                                                                     
   ).patch_name;                                                                                                                                      
                                                                                                                                                      
END patch_name;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- patch_name - retrieved via unique index patch_prereqs_uk1                                                                                          
-----------------------------------------------------------------                                                                                     
FUNCTION patch_name (                                                                                                                                 
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.patch_name%TYPE                                                                                                            
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_uk1(                                                                                                                           
   i_patch_name   => i_patch_name                                                                                                                     
  ,i_prereq_patch => i_prereq_patch                                                                                                                   
,i_raise_error => i_raise_error                                                                                                                       
   ).patch_name;                                                                                                                                      
                                                                                                                                                      
END patch_name;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- prereq_patch - retrieved via rowid                                                                                                                 
-----------------------------------------------------------------                                                                                     
FUNCTION prereq_patch (                                                                                                                               
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.prereq_patch%TYPE                                                                                                          
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_rid (                                                                                                                          
   i_rowid => i_rowid                                                                                                                                 
  ,i_raise_error => i_raise_error                                                                                                                     
   ).prereq_patch;                                                                                                                                    
                                                                                                                                                      
END prereq_patch;                                                                                                                                     
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- prereq_patch - retrieved via primary key patch_prereqs_pk                                                                                          
-----------------------------------------------------------------                                                                                     
FUNCTION prereq_patch (                                                                                                                               
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.prereq_patch%TYPE                                                                                                          
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_pk (                                                                                                                           
   i_patch_prereq_id => i_patch_prereq_id                                                                                                             
  ,i_raise_error => i_raise_error                                                                                                                     
   ).prereq_patch;                                                                                                                                    
                                                                                                                                                      
END prereq_patch;                                                                                                                                     
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- prereq_patch - retrieved via unique index patch_prereqs_uk1                                                                                        
-----------------------------------------------------------------                                                                                     
FUNCTION prereq_patch (                                                                                                                               
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.prereq_patch%TYPE                                                                                                          
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_uk1(                                                                                                                           
   i_patch_name   => i_patch_name                                                                                                                     
  ,i_prereq_patch => i_prereq_patch                                                                                                                   
,i_raise_error => i_raise_error                                                                                                                       
   ).prereq_patch;                                                                                                                                    
                                                                                                                                                      
END prereq_patch;                                                                                                                                     
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_by - retrieved via rowid                                                                                                                   
-----------------------------------------------------------------                                                                                     
FUNCTION created_by (                                                                                                                                 
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_by%TYPE                                                                                                            
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_rid (                                                                                                                          
   i_rowid => i_rowid                                                                                                                                 
  ,i_raise_error => i_raise_error                                                                                                                     
   ).created_by;                                                                                                                                      
                                                                                                                                                      
END created_by;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_by - retrieved via primary key patch_prereqs_pk                                                                                            
-----------------------------------------------------------------                                                                                     
FUNCTION created_by (                                                                                                                                 
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_by%TYPE                                                                                                            
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_pk (                                                                                                                           
   i_patch_prereq_id => i_patch_prereq_id                                                                                                             
  ,i_raise_error => i_raise_error                                                                                                                     
   ).created_by;                                                                                                                                      
                                                                                                                                                      
END created_by;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_by - retrieved via unique index patch_prereqs_uk1                                                                                          
-----------------------------------------------------------------                                                                                     
FUNCTION created_by (                                                                                                                                 
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_by%TYPE                                                                                                            
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_uk1(                                                                                                                           
   i_patch_name   => i_patch_name                                                                                                                     
  ,i_prereq_patch => i_prereq_patch                                                                                                                   
,i_raise_error => i_raise_error                                                                                                                       
   ).created_by;                                                                                                                                      
                                                                                                                                                      
END created_by;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_on - retrieved via rowid                                                                                                                   
-----------------------------------------------------------------                                                                                     
FUNCTION created_on (                                                                                                                                 
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_on%TYPE                                                                                                            
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_rid (                                                                                                                          
   i_rowid => i_rowid                                                                                                                                 
  ,i_raise_error => i_raise_error                                                                                                                     
   ).created_on;                                                                                                                                      
                                                                                                                                                      
END created_on;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_on - retrieved via primary key patch_prereqs_pk                                                                                            
-----------------------------------------------------------------                                                                                     
FUNCTION created_on (                                                                                                                                 
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_on%TYPE                                                                                                            
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_pk (                                                                                                                           
   i_patch_prereq_id => i_patch_prereq_id                                                                                                             
  ,i_raise_error => i_raise_error                                                                                                                     
   ).created_on;                                                                                                                                      
                                                                                                                                                      
END created_on;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- created_on - retrieved via unique index patch_prereqs_uk1                                                                                          
-----------------------------------------------------------------                                                                                     
FUNCTION created_on (                                                                                                                                 
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.created_on%TYPE                                                                                                            
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_uk1(                                                                                                                           
   i_patch_name   => i_patch_name                                                                                                                     
  ,i_prereq_patch => i_prereq_patch                                                                                                                   
,i_raise_error => i_raise_error                                                                                                                       
   ).created_on;                                                                                                                                      
                                                                                                                                                      
END created_on;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_by - retrieved via rowid                                                                                                              
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_by (                                                                                                                            
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_by%TYPE                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_rid (                                                                                                                          
   i_rowid => i_rowid                                                                                                                                 
  ,i_raise_error => i_raise_error                                                                                                                     
   ).last_updated_by;                                                                                                                                 
                                                                                                                                                      
END last_updated_by;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_by - retrieved via primary key patch_prereqs_pk                                                                                       
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_by (                                                                                                                            
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_by%TYPE                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_pk (                                                                                                                           
   i_patch_prereq_id => i_patch_prereq_id                                                                                                             
  ,i_raise_error => i_raise_error                                                                                                                     
   ).last_updated_by;                                                                                                                                 
                                                                                                                                                      
END last_updated_by;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_by - retrieved via unique index patch_prereqs_uk1                                                                                     
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_by (                                                                                                                            
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_by%TYPE                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_uk1(                                                                                                                           
   i_patch_name   => i_patch_name                                                                                                                     
  ,i_prereq_patch => i_prereq_patch                                                                                                                   
,i_raise_error => i_raise_error                                                                                                                       
   ).last_updated_by;                                                                                                                                 
                                                                                                                                                      
END last_updated_by;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_on - retrieved via rowid                                                                                                              
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_on (                                                                                                                            
   i_rowid IN rowid                                                                                                                                   
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_on%TYPE                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_rid (                                                                                                                          
   i_rowid => i_rowid                                                                                                                                 
  ,i_raise_error => i_raise_error                                                                                                                     
   ).last_updated_on;                                                                                                                                 
                                                                                                                                                      
END last_updated_on;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_on - retrieved via primary key patch_prereqs_pk                                                                                       
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_on (                                                                                                                            
   i_patch_prereq_id IN patch_prereqs.patch_prereq_id%TYPE                                                                                            
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_on%TYPE                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_pk (                                                                                                                           
   i_patch_prereq_id => i_patch_prereq_id                                                                                                             
  ,i_raise_error => i_raise_error                                                                                                                     
   ).last_updated_on;                                                                                                                                 
                                                                                                                                                      
END last_updated_on;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- last_updated_on - retrieved via unique index patch_prereqs_uk1                                                                                     
-----------------------------------------------------------------                                                                                     
FUNCTION last_updated_on (                                                                                                                            
   i_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                                    
  ,i_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                                  
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )  RETURN patch_prereqs.last_updated_on%TYPE                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  RETURN patch_prereqs_uk1(                                                                                                                           
   i_patch_name   => i_patch_name                                                                                                                     
  ,i_prereq_patch => i_prereq_patch                                                                                                                   
,i_raise_error => i_raise_error                                                                                                                       
   ).last_updated_on;                                                                                                                                 
                                                                                                                                                      
END last_updated_on;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- get_current_rec                                                                                                                                    
-----------------------------------------------------------------                                                                                     
-- get the current record by pk if given, otherwise by uk1                                                                                            
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
FUNCTION get_current_rec(                                                                                                                             
             i_patch_prereqs IN patch_prereqs%rowtype                                                                                                 
            ,i_raise_error IN VARCHAR2 DEFAULT 'Y'                                                                                                    
) RETURN patch_prereqs%rowtype IS                                                                                                                     
                                                                                                                                                      
   l_patch_prereqs   patch_prereqs%rowtype := i_patch_prereqs;                                                                                        
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  if l_patch_prereqs.patch_prereq_id is not null then                                                                                                 
    --use pk to load original record                                                                                                                  
    l_patch_prereqs := patch_prereqs_tapi.patch_prereqs_pk(                                                                                           
     i_patch_prereq_id => i_patch_prereqs.patch_prereq_id                                                                                             
  ,i_raise_error => i_raise_error);                                                                                                                   
                                                                                                                                                      
  end if;                                                                                                                                             
                                                                                                                                                      
                                                                                                                                                      
  if l_patch_prereqs.patch_prereq_id is null then                                                                                                     
  --use index patch_prereqs_uk1 to load original record                                                                                               
    l_patch_prereqs := patch_prereqs_tapi.patch_prereqs_uk1(                                                                                          
     i_patch_name   => i_patch_prereqs.patch_name                                                                                                     
    ,i_prereq_patch => i_patch_prereqs.prereq_patch                                                                                                   
   ,i_raise_error => i_raise_error                                                                                                                    
    );                                                                                                                                                
  end if;                                                                                                                                             
                                                                                                                                                      
                                                                                                                                                      
  return l_patch_prereqs;                                                                                                                             
                                                                                                                                                      
END get_current_rec;                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
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
) RETURN patch_prereqs%ROWTYPE IS                                                                                                                     
                                                                                                                                                      
   l_patch_prereqs             patch_prereqs%rowtype;                                                                                                 
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
   l_patch_prereqs.patch_prereq_id := i_patch_prereq_id;                                                                                              
   l_patch_prereqs.patch_name      := i_patch_name     ;                                                                                              
   l_patch_prereqs.prereq_patch    := i_prereq_patch   ;                                                                                              
   l_patch_prereqs.created_by      := i_created_by     ;                                                                                              
   l_patch_prereqs.created_on      := i_created_on     ;                                                                                              
   l_patch_prereqs.last_updated_by := i_last_updated_by;                                                                                              
   l_patch_prereqs.last_updated_on := i_last_updated_on;                                                                                              
                                                                                                                                                      
  return l_patch_prereqs;                                                                                                                             
                                                                                                                                                      
END create_rec;                                                                                                                                       
                                                                                                                                                      
                                                                                                                                                      
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
) IS                                                                                                                                                  
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
   o_patch_prereq_id := i_patch_prereqs.patch_prereq_id;                                                                                              
   o_patch_name      := i_patch_prereqs.patch_name;                                                                                                   
   o_prereq_patch    := i_patch_prereqs.prereq_patch;                                                                                                 
   o_created_by      := i_patch_prereqs.created_by;                                                                                                   
   o_created_on      := i_patch_prereqs.created_on;                                                                                                   
   o_last_updated_by := i_patch_prereqs.last_updated_by;                                                                                              
   o_last_updated_on := i_patch_prereqs.last_updated_on;                                                                                              
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- merge_old_and_new                                                                                                                                  
-----------------------------------------------------------------                                                                                     
-- null values in NEW replaced with values from OLD                                                                                                   
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE merge_old_and_new(i_old_rec  IN     patch_prereqs%rowtype                                                                                   
                           ,io_new_rec IN OUT patch_prereqs%rowtype) IS                                                                               
BEGIN                                                                                                                                                 
                                                                                                                                                      
  io_new_rec.patch_prereq_id := NVL(io_new_rec.patch_prereq_id,i_old_rec.patch_prereq_id);                                                            
  io_new_rec.patch_name      := NVL(io_new_rec.patch_name     ,i_old_rec.patch_name     );                                                            
  io_new_rec.prereq_patch    := NVL(io_new_rec.prereq_patch   ,i_old_rec.prereq_patch   );                                                            
  io_new_rec.created_by      := NVL(io_new_rec.created_by     ,i_old_rec.created_by     );                                                            
  io_new_rec.created_on      := NVL(io_new_rec.created_on     ,i_old_rec.created_on     );                                                            
  io_new_rec.last_updated_by := NVL(io_new_rec.last_updated_by,i_old_rec.last_updated_by);                                                            
  io_new_rec.last_updated_on := NVL(io_new_rec.last_updated_on,i_old_rec.last_updated_on);                                                            
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- INSERT                                                                                                                                             
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- ins                                                                                                                                                
-----------------------------------------------------------------                                                                                     
-- insert a record - using record type, returning record                                                                                              
--   uses returing clause.                                                                                                                            
--   Unsuitable for tables with a long column                                                                                                         
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE ins(                                                                                                                                        
    io_patch_prereqs  in out patch_prereqs%rowtype )                                                                                                  
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  INSERT INTO patch_prereqs VALUES io_patch_prereqs                                                                                                   
  RETURNING                                                                                                                                           
     patch_prereq_id                                                                                                                                  
    ,patch_name                                                                                                                                       
    ,prereq_patch                                                                                                                                     
    ,created_by                                                                                                                                       
    ,created_on                                                                                                                                       
    ,last_updated_by                                                                                                                                  
    ,last_updated_on                                                                                                                                  
  INTO io_patch_prereqs;                                                                                                                              
                                                                                                                                                      
END ins;                                                                                                                                              
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
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
) IS                                                                                                                                                  
                                                                                                                                                      
   l_patch_prereqs             patch_prereqs%rowtype;                                                                                                 
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  l_patch_prereqs := create_rec(                                                                                                                      
     io_patch_prereq_id                                                                                                                               
    ,io_patch_name                                                                                                                                    
    ,io_prereq_patch                                                                                                                                  
    ,io_created_by                                                                                                                                    
    ,io_created_on                                                                                                                                    
    ,io_last_updated_by                                                                                                                               
    ,io_last_updated_on                                                                                                                               
 );                                                                                                                                                   
                                                                                                                                                      
  ins(io_patch_prereqs => l_patch_prereqs);                                                                                                           
                                                                                                                                                      
  split_rec( l_patch_prereqs,                                                                                                                         
     io_patch_prereq_id                                                                                                                               
    ,io_patch_name                                                                                                                                    
    ,io_prereq_patch                                                                                                                                  
    ,io_created_by                                                                                                                                    
    ,io_created_on                                                                                                                                    
    ,io_last_updated_by                                                                                                                               
    ,io_last_updated_on                                                                                                                               
);                                                                                                                                                    
                                                                                                                                                      
END ins;                                                                                                                                              
                                                                                                                                                      
                                                                                                                                                      
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
)                                                                                                                                                     
IS                                                                                                                                                    
                                                                                                                                                      
   l_patch_prereqs             patch_prereqs%rowtype;                                                                                                 
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  l_patch_prereqs := create_rec(                                                                                                                      
     i_patch_prereq_id                                                                                                                                
    ,i_patch_name                                                                                                                                     
    ,i_prereq_patch                                                                                                                                   
    ,i_created_by                                                                                                                                     
    ,i_created_on                                                                                                                                     
    ,i_last_updated_by                                                                                                                                
    ,i_last_updated_on                                                                                                                                
 );                                                                                                                                                   
                                                                                                                                                      
  ins(io_patch_prereqs => l_patch_prereqs);                                                                                                           
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- UPDATE                                                                                                                                             
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- upd                                                                                                                                                
-----------------------------------------------------------------                                                                                     
-- update a record - using record type, primary key cols, returning record                                                                            
--   uses returing clause.                                                                                                                            
--   Unsuitable for tables with a long column                                                                                                         
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE upd(                                                                                                                                        
    io_patch_prereqs  in out patch_prereqs%rowtype )                                                                                                  
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  UPDATE patch_prereqs SET ROW = io_patch_prereqs                                                                                                     
       WHERE patch_prereq_id = io_patch_prereqs.patch_prereq_id                                                                                       
  RETURNING                                                                                                                                           
     patch_prereq_id                                                                                                                                  
    ,patch_name                                                                                                                                       
    ,prereq_patch                                                                                                                                     
    ,created_by                                                                                                                                       
    ,created_on                                                                                                                                       
    ,last_updated_by                                                                                                                                  
    ,last_updated_on                                                                                                                                  
  INTO io_patch_prereqs;                                                                                                                              
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
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
)                                                                                                                                                     
IS                                                                                                                                                    
                                                                                                                                                      
   l_patch_prereqs            patch_prereqs%rowtype;                                                                                                  
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  l_patch_prereqs := create_rec(                                                                                                                      
     io_patch_prereq_id                                                                                                                               
    ,io_patch_name                                                                                                                                    
    ,io_prereq_patch                                                                                                                                  
    ,io_created_by                                                                                                                                    
    ,io_created_on                                                                                                                                    
    ,io_last_updated_by                                                                                                                               
    ,io_last_updated_on                                                                                                                               
 );                                                                                                                                                   
                                                                                                                                                      
  upd(io_patch_prereqs => l_patch_prereqs);                                                                                                           
                                                                                                                                                      
  split_rec( l_patch_prereqs,                                                                                                                         
     io_patch_prereq_id                                                                                                                               
    ,io_patch_name                                                                                                                                    
    ,io_prereq_patch                                                                                                                                  
    ,io_created_by                                                                                                                                    
    ,io_created_on                                                                                                                                    
    ,io_last_updated_by                                                                                                                               
    ,io_last_updated_on                                                                                                                               
);                                                                                                                                                    
                                                                                                                                                      
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
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
)                                                                                                                                                     
IS                                                                                                                                                    
                                                                                                                                                      
   l_new_patch_prereqs  patch_prereqs%rowtype;                                                                                                        
   l_old_patch_prereqs  patch_prereqs%rowtype;                                                                                                        
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  l_new_patch_prereqs := create_rec(                                                                                                                  
     i_patch_prereq_id                                                                                                                                
    ,i_patch_name                                                                                                                                     
    ,i_prereq_patch                                                                                                                                   
    ,i_created_by                                                                                                                                     
    ,i_created_on                                                                                                                                     
    ,i_last_updated_by                                                                                                                                
    ,i_last_updated_on                                                                                                                                
 );                                                                                                                                                   
                                                                                                                                                      
  l_old_patch_prereqs := get_current_rec( i_patch_prereqs =>  l_new_patch_prereqs                                                                     
                                       ,i_raise_error =>  'Y');                                                                                       
                                                                                                                                                      
  merge_old_and_new(i_old_rec  => l_old_patch_prereqs                                                                                                 
                   ,io_new_rec => l_new_patch_prereqs);                                                                                               
                                                                                                                                                      
  upd(io_patch_prereqs => l_new_patch_prereqs);                                                                                                       
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- upd_patch_prereqs_uk1 - use uk to update itself                                                                                                    
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
PROCEDURE upd_patch_prereqs_uk1 (                                                                                                                     
     i_old_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                              
    ,i_old_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                            
      ,i_new_patch_name   IN patch_prereqs.patch_name%TYPE                                                                                            
                                                                                                                                                      
    ,i_new_prereq_patch IN patch_prereqs.prereq_patch%TYPE                                                                                            
                                                                                                                                                      
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'                                                                                                              
   )                                                                                                                                                  
IS                                                                                                                                                    
                                                                                                                                                      
   x_unknown_key EXCEPTION;                                                                                                                           
BEGIN                                                                                                                                                 
                                                                                                                                                      
  UPDATE patch_prereqs                                                                                                                                
     SET patch_name   = i_new_patch_name                                                                                                              
       , prereq_patch = i_new_prereq_patch                                                                                                            
       WHERE patch_name   = i_old_patch_name                                                                                                          
     AND prereq_patch = i_old_prereq_patch                                                                                                            
  ;                                                                                                                                                   
                                                                                                                                                      
   IF SQL%ROWCOUNT = 0 AND                                                                                                                            
      i_raise_error = 'Y' THEN                                                                                                                        
     RAISE x_unknown_key;                                                                                                                             
   END IF;                                                                                                                                            
                                                                                                                                                      
EXCEPTION                                                                                                                                             
  WHEN x_unknown_key THEN                                                                                                                             
--    add_log('patch_prereqs_uk1: Unknown key value'||                                                                                                
--[FOREACH]uindcol[between]||                                                                                                                         
--    ' i_[colname/l]='||i_old_[colname/l]                                                                                                            
--[ENDFOREACH]                                                                                                                                        
--    );                                                                                                                                              
    RAISE NO_DATA_FOUND;                                                                                                                              
                                                                                                                                                      
END upd_patch_prereqs_uk1;                                                                                                                            
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- DELETE                                                                                                                                             
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
-----------------------------------------------------------------                                                                                     
-- del                                                                                                                                                
-----------------------------------------------------------------                                                                                     
-- delete a record - using record type                                                                                                                
-----------------------------------------------------------------                                                                                     
                                                                                                                                                      
                                                                                                                                                      
PROCEDURE del(                                                                                                                                        
    i_patch_prereqs  in patch_prereqs%rowtype )                                                                                                       
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  DELETE patch_prereqs                                                                                                                                
     WHERE patch_prereq_id = i_patch_prereqs.patch_prereq_id                                                                                          
  ;                                                                                                                                                   
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
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
)                                                                                                                                                     
IS                                                                                                                                                    
   l_new_patch_prereqs  patch_prereqs%rowtype;                                                                                                        
   l_old_patch_prereqs  patch_prereqs%rowtype;                                                                                                        
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
                                                                                                                                                      
  l_new_patch_prereqs := create_rec(                                                                                                                  
     i_patch_prereq_id                                                                                                                                
    ,i_patch_name                                                                                                                                     
    ,i_prereq_patch                                                                                                                                   
    ,i_created_by                                                                                                                                     
    ,i_created_on                                                                                                                                     
    ,i_last_updated_by                                                                                                                                
    ,i_last_updated_on                                                                                                                                
 );                                                                                                                                                   
                                                                                                                                                      
  l_old_patch_prereqs := get_current_rec( i_patch_prereqs =>  l_new_patch_prereqs                                                                     
                                       ,i_raise_error =>  i_raise_error);                                                                             
                                                                                                                                                      
  del(i_patch_prereqs => l_old_patch_prereqs);                                                                                                        
                                                                                                                                                      
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
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
    io_patch_prereqs  in out patch_prereqs%rowtype )                                                                                                  
IS                                                                                                                                                    
                                                                                                                                                      
BEGIN                                                                                                                                                 
  BEGIN                                                                                                                                               
    --Insert                                                                                                                                          
    patch_prereqs_tapi.ins( io_patch_prereqs => io_patch_prereqs );                                                                                   
                                                                                                                                                      
  EXCEPTION                                                                                                                                           
    WHEN DUP_VAL_ON_INDEX THEN                                                                                                                        
                                                                                                                                                      
      --update                                                                                                                                        
                                                                                                                                                      
      --Get primary key value for patch_prereqs                                                                                                       
      io_patch_prereqs.patch_prereq_id := get_current_rec( i_patch_prereqs =>  io_patch_prereqs                                                       
                                                  ,i_raise_error =>  'Y').patch_prereq_id;                                                            
                                                                                                                                                      
      --Update                                                                                                                                        
      patch_prereqs_tapi.upd( io_patch_prereqs => io_patch_prereqs );                                                                                 
                                                                                                                                                      
  END;                                                                                                                                                
END;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
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
) IS                                                                                                                                                  
                                                                                                                                                      
   l_patch_prereqs             patch_prereqs%rowtype;                                                                                                 
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  l_patch_prereqs := create_rec(                                                                                                                      
     io_patch_prereq_id                                                                                                                               
    ,io_patch_name                                                                                                                                    
    ,io_prereq_patch                                                                                                                                  
    ,io_created_by                                                                                                                                    
    ,io_created_on                                                                                                                                    
    ,io_last_updated_by                                                                                                                               
    ,io_last_updated_on                                                                                                                               
 );                                                                                                                                                   
                                                                                                                                                      
  ins_upd(io_patch_prereqs => l_patch_prereqs);                                                                                                       
                                                                                                                                                      
  split_rec( l_patch_prereqs,                                                                                                                         
     io_patch_prereq_id                                                                                                                               
    ,io_patch_name                                                                                                                                    
    ,io_prereq_patch                                                                                                                                  
    ,io_created_by                                                                                                                                    
    ,io_created_on                                                                                                                                    
    ,io_last_updated_by                                                                                                                               
    ,io_last_updated_on                                                                                                                               
);                                                                                                                                                    
                                                                                                                                                      
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
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
) IS                                                                                                                                                  
                                                                                                                                                      
BEGIN                                                                                                                                                 
                                                                                                                                                      
  ins_opt(                                                                                                                                            
     i_patch_prereq_id                                                                                                                                
    ,i_patch_name                                                                                                                                     
    ,i_prereq_patch                                                                                                                                   
    ,i_created_by                                                                                                                                     
    ,i_created_on                                                                                                                                     
    ,i_last_updated_by                                                                                                                                
    ,i_last_updated_on                                                                                                                                
 );                                                                                                                                                   
                                                                                                                                                      
  EXCEPTION                                                                                                                                           
    WHEN DUP_VAL_ON_INDEX THEN                                                                                                                        
      --update                                                                                                                                        
  upd_not_null(                                                                                                                                       
     i_patch_prereq_id                                                                                                                                
    ,i_patch_name                                                                                                                                     
    ,i_prereq_patch                                                                                                                                   
    ,i_created_by                                                                                                                                     
    ,i_created_on                                                                                                                                     
    ,i_last_updated_by                                                                                                                                
    ,i_last_updated_on                                                                                                                                
 );                                                                                                                                                   
                                                                                                                                                      
END;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- DATA UNLOADING                                                                                                                                     
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
                                                                                                                                                      
------------------------------------------------------------------------------                                                                        
-- unload_data                                                                                                                                        
------------------------------------------------------------------------------                                                                        
-- unload data into a script ins_upd statements                                                                                                       
-- in PK order if possible..                                                                                                                          
-- return this script as a clob?                                                                                                                      
-- or could be a pipelined table function, that is spooled from SQL                                                                                   
------------------------------------------------------------------------------                                                                        
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
procedure unload_data is                                                                                                                              
                                                                                                                                                      
  l_template clob :=                                                                                                                                  
q'!                                                                                                                                                   
patch_prereqs_tapi.ins_upd_not_null(                                                                                                                  
     i_patch_prereq_id => '{PATCH_PREREQ_ID}'                                                                                                         
    ,i_patch_name      => '{PATCH_NAME}'                                                                                                              
    ,i_prereq_patch    => '{PREREQ_PATCH}'                                                                                                            
    ,i_created_by      => '{CREATED_BY}'                                                                                                              
    ,i_created_on      => '{CREATED_ON}'                                                                                                              
    ,i_last_updated_by => '{LAST_UPDATED_BY}'                                                                                                         
    ,i_last_updated_on => '{LAST_UPDATED_ON}'                                                                                                         
);                                                                                                                                                    
!';                                                                                                                                                   
                                                                                                                                                      
                                                                                                                                                      
begin                                                                                                                                                 
  dbms_output.put_line('PROMPT Reloading data into patch_prereqs');                                                                                   
  dbms_output.put_line('BEGIN');                                                                                                                      
  for l_patch_prereqs in (                                                                                                                            
     select *                                                                                                                                         
     from patch_prereqs                                                                                                                               
     order by                                                                                                                                         
              patch_prereq_id       ) loop                                                                                                            
                                                                                                                                                      
    declare                                                                                                                                           
      l_ins_upd CLOB := l_template;                                                                                                                   
                                                                                                                                                      
    begin                                                                                                                                             
      l_ins_upd   := REPLACE(l_ins_upd, '{PATCH_PREREQ_ID}' , l_patch_prereqs.patch_prereq_id);                                                       
      l_ins_upd   := REPLACE(l_ins_upd, '{PATCH_NAME}' , l_patch_prereqs.patch_name);                                                                 
      l_ins_upd   := REPLACE(l_ins_upd, '{PREREQ_PATCH}' , l_patch_prereqs.prereq_patch);                                                             
      l_ins_upd   := REPLACE(l_ins_upd, '{CREATED_BY}' , l_patch_prereqs.created_by);                                                                 
      l_ins_upd   := REPLACE(l_ins_upd, '{CREATED_ON}' , l_patch_prereqs.created_on);                                                                 
      l_ins_upd   := REPLACE(l_ins_upd, '{LAST_UPDATED_BY}' , l_patch_prereqs.last_updated_by);                                                       
      l_ins_upd   := REPLACE(l_ins_upd, '{LAST_UPDATED_ON}' , l_patch_prereqs.last_updated_on);                                                       
                                                                                                                                                      
      dbms_output.put_line(l_ins_upd);                                                                                                                
    end;                                                                                                                                              
                                                                                                                                                      
  end loop;                                                                                                                                           
  dbms_output.put_line('END;');                                                                                                                       
  dbms_output.put_line('/');                                                                                                                          
  dbms_output.put_line('PROMPT Dataload complete!');                                                                                                  
                                                                                                                                                      
end;                                                                                                                                                  
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
                                                                                                                                                      
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
RETURN BOOLEAN                                                                                                                                        
IS                                                                                                                                                    
l_index1   PLS_INTEGER := i_collection1.FIRST;                                                                                                        
l_index2   PLS_INTEGER := i_collection2.FIRST;                                                                                                        
l_collections_equal     BOOLEAN     DEFAULT TRUE;                                                                                                     
                                                                                                                                                      
  FUNCTION equal_records ( rec1_in IN patch_prereqs%ROWTYPE                                                                                           
                         , rec2_in IN patch_prereqs%ROWTYPE ) RETURN BOOLEAN                                                                          
  IS                                                                                                                                                  
    retval BOOLEAN;                                                                                                                                   
  BEGIN                                                                                                                                               
    retval := rec1_in.patch_prereq_id = rec2_in.patch_prereq_id OR                                                                                    
   (rec1_in.patch_prereq_id IS NULL AND rec2_in.patch_prereq_id IS NULL);                                                                             
IF NOT NVL (retval, FALSE) THEN                                                                                                                       
  RETURN FALSE;                                                                                                                                       
END IF;                                                                                                                                               
    retval := rec1_in.patch_name = rec2_in.patch_name OR                                                                                              
   (rec1_in.patch_name IS NULL AND rec2_in.patch_name IS NULL);                                                                                       
IF NOT NVL (retval, FALSE) THEN                                                                                                                       
  RETURN FALSE;                                                                                                                                       
END IF;                                                                                                                                               
    retval := rec1_in.prereq_patch = rec2_in.prereq_patch OR                                                                                          
   (rec1_in.prereq_patch IS NULL AND rec2_in.prereq_patch IS NULL);                                                                                   
IF NOT NVL (retval, FALSE) THEN                                                                                                                       
  RETURN FALSE;                                                                                                                                       
END IF;                                                                                                                                               
    retval := rec1_in.created_by = rec2_in.created_by OR                                                                                              
   (rec1_in.created_by IS NULL AND rec2_in.created_by IS NULL);                                                                                       
IF NOT NVL (retval, FALSE) THEN                                                                                                                       
  RETURN FALSE;                                                                                                                                       
END IF;                                                                                                                                               
    retval := rec1_in.created_on = rec2_in.created_on OR                                                                                              
   (rec1_in.created_on IS NULL AND rec2_in.created_on IS NULL);                                                                                       
IF NOT NVL (retval, FALSE) THEN                                                                                                                       
  RETURN FALSE;                                                                                                                                       
END IF;                                                                                                                                               
    retval := rec1_in.last_updated_by = rec2_in.last_updated_by OR                                                                                    
   (rec1_in.last_updated_by IS NULL AND rec2_in.last_updated_by IS NULL);                                                                             
IF NOT NVL (retval, FALSE) THEN                                                                                                                       
  RETURN FALSE;                                                                                                                                       
END IF;                                                                                                                                               
    retval := rec1_in.last_updated_on = rec2_in.last_updated_on OR                                                                                    
   (rec1_in.last_updated_on IS NULL AND rec2_in.last_updated_on IS NULL);                                                                             
IF NOT NVL (retval, FALSE) THEN                                                                                                                       
  RETURN FALSE;                                                                                                                                       
END IF;                                                                                                                                               
                                                                                                                                                      
    RETURN NVL (retval, FALSE);                                                                                                                       
                                                                                                                                                      
  END equal_records;                                                                                                                                  
                                                                                                                                                      
BEGIN                                                                                                                                                 
-- Are both collections empty?                                                                                                                        
IF l_index1 IS NULL AND l_index2 IS NULL                                                                                                              
THEN                                                                                                                                                  
  l_collections_equal := NVL (i_both_null_true, FALSE);                                                                                               
                                                                                                                                                      
-- Is only one empty?                                                                                                                                 
ELSIF    (l_index1 IS NULL AND l_index2 IS NOT NULL)                                                                                                  
     OR (l_index1 IS NOT NULL AND l_index2 IS NULL)                                                                                                   
THEN                                                                                                                                                  
  l_collections_equal := FALSE;                                                                                                                       
ELSE                                                                                                                                                  
  -- Start the row by row comparisons.                                                                                                                
  WHILE (l_index1 IS NOT NULL AND l_index2 IS NOT NULL AND l_collections_equal)                                                                       
  LOOP                                                                                                                                                
     -- Compare each field of both records. Are the individual values equal?                                                                          
     -- Do the values match? And if for any reason, this evaluates to NULL,                                                                           
     -- then treat it as FALSE.                                                                                                                       
     l_collections_equal :=                                                                                                                           
        equal_records (i_collection1 (l_index1)                                                                                                       
                     , i_collection2 (l_index2));                                                                                                     
                                                                                                                                                      
     -- Do the indexes match (if that is requested)? And if for any reason,                                                                           
     -- this evaluates to NULL, then treat it as FALSE.                                                                                               
     IF l_collections_equal AND i_match_indexes                                                                                                       
     THEN                                                                                                                                             
        l_collections_equal := NVL (l_index1 = l_index2, FALSE);                                                                                      
     END IF;                                                                                                                                          
                                                                                                                                                      
     -- If still equal, go to next element in each collection                                                                                         
     -- and make sure they both still have a value.                                                                                                   
     IF l_collections_equal                                                                                                                           
     THEN                                                                                                                                             
        l_index1 := i_collection1.NEXT (l_index1);                                                                                                    
        l_index2 := i_collection2.NEXT (l_index2);                                                                                                    
        l_collections_equal :=                                                                                                                        
              (l_index1 IS NOT NULL AND l_index2 IS NOT NULL                                                                                          
              )                                                                                                                                       
           OR (l_index1 IS NULL AND l_index2 IS NULL);                                                                                                
     END IF;                                                                                                                                          
  END LOOP;                                                                                                                                           
END IF;                                                                                                                                               
                                                                                                                                                      
RETURN l_collections_equal;                                                                                                                           
END collections_equal;                                                                                                                                
                                                                                                                                                      
                                                                                                                                                      
end patch_prereqs_tapi;                                                                                                                               
/
show error;
