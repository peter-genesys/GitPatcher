Public Class GPTrees
 

    Shared Sub showCheckedNodesButton_Click(ByRef aTreeView As TreeView)
        ' Disable redrawing of treeView1 to prevent flickering  
        ' while changes are made.
        aTreeView.BeginUpdate()

        ' Collapse all nodes of treeView1.
        aTreeView.CollapseAll()

        ' Add the CheckForCheckedChildren event handler to the BeforeExpand event. 
        AddHandler aTreeView.BeforeExpand, AddressOf CheckForCheckedChildren

        ' Expand all nodes of treeView1. Nodes without checked children are  
        ' prevented from expanding by the checkForCheckedChildren event handler.
        aTreeView.ExpandAll()

        ' Remove the checkForCheckedChildren event handler from the BeforeExpand  
        ' event so manual node expansion will work correctly. 
        RemoveHandler aTreeView.BeforeExpand, AddressOf CheckForCheckedChildren

        ' Enable redrawing of treeView1.
        aTreeView.EndUpdate()
    End Sub 'showCheckedNodesButton_Click

    ' Prevent expansion of a node that does not have any checked child nodes. 
    Shared Sub CheckForCheckedChildren(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs)
        If Not HasCheckedChildNodes(e.Node) Then
            e.Cancel = True
        End If
    End Sub 'CheckForCheckedChildren

    ' Returns a value indicating whether the specified  
    ' TreeNode has checked child nodes. 
    Shared Function HasCheckedChildNodes(ByVal node As TreeNode) As Boolean
        If node.Nodes.Count = 0 Then
            Return False
        End If
        Dim childNode As TreeNode
        For Each childNode In node.Nodes
            If childNode.Checked Then
                Return True
            End If
            ' Recursively check the children of the current child node. 
            If HasCheckedChildNodes(childNode) Then
                Return True
            End If
        Next childNode
        Return False
    End Function 'HasCheckedChildNodes


    Shared Sub treeChange_Click(ByRef sender As Object, ByRef aTreeView As TreeView)

        If sender.text = "Collapse" Then
            sender.text = "Expand"
            aTreeView.CollapseAll()
        ElseIf sender.text = "Expand" Then
            sender.text = "Contract"
            aTreeView.ExpandAll()
        ElseIf sender.text = "Contract" Then
            sender.text = "Collapse"
            showCheckedNodesButton_Click(aTreeView)
        End If

    End Sub


    Shared Function AddNode(ByRef nodes As TreeNodeCollection, ByVal fullPath As String, ByVal remainderPath As String, Optional ByVal delim As String = "\", Optional ByVal checked As Boolean = False) As Boolean
        'Commented out the logging, as it severely reduces performance.
        Dim first_segment As String = Common.getFirstSegment(remainderPath, delim)
        Dim remainder As String = Common.dropFirstSegment(remainderPath, delim)

        Dim lFound As Boolean = False

        'First try to find the node
        For Each node In nodes
            'Logger.Note("node.FullPath", node.FullPath)
            ' Logger.Note("fullPath", fullPath)
            'Logger.Note("InStr", InStr(fullPath, node.FullPath))
            If fullPath = node.FullPath Then
                'If node.FullPath.ToString = fullPath Then
                'Yay found it nothing to do
                'Logger.Dbg("Yay found it nothing to do")
                Return True
                'Node Full path must match first part of given full path, and current node must match exactly current segment
            ElseIf InStr(fullPath, node.FullPath.ToString) = 1 And first_segment = node.text Then
                'Found a parent node at least, lets look for children
                'Logger.Dbg("Found a parent node at least, lets look for children")
                lFound = AddNode(node.nodes, fullPath, remainder, delim, checked)
            End If

        Next

        If Not lFound And Not String.IsNullOrEmpty(first_segment) Then
            'Need to make a node
            'Logger.Dbg("Need to make a node for " & first_segment)
            Dim newNode As TreeNode = New TreeNode(first_segment)
            newNode.Checked = checked
            nodes.Add(newNode)
            'If newNode.FullPath = fullPath Then
            If String.IsNullOrEmpty(remainder) Then
                'We made the node!
                'Logger.Dbg("We made the node!")
                lFound = True
            Else
                'Now follow this child
                'Logger.Dbg("Now follow this child")
                'newNode.  .ShowCheckBox = False 'Hide checkbox of any parent node.
                lFound = AddNode(newNode.Nodes, fullPath, remainder, delim, checked)
            End If

        End If
        If Not lFound Then
            MsgBox("Oops not found. Bad coding?")
        End If
        Return lFound



    End Function
 


    Shared Sub populateTreeFromCollection(ByRef patchesTreeView As TreeView, ByRef patches As Collection)

        patchesTreeView.PathSeparator = "\"
        patchesTreeView.Nodes.Clear()

        'copy each item from listbox

        For Each patch In patches

            'find or create each node for item
            GPTrees.AddNode(patchesTreeView.Nodes, patch, patch)

        Next

    End Sub

    Shared Sub populateTreeFromListbox(ByRef patchesTreeView As TreeView, ByRef patchesListBox As ListBox)

        patchesTreeView.PathSeparator = "\"
        patchesTreeView.Nodes.Clear()
 
        ''copy each item from listbox
        'For i As Integer = 0 To patchesListBox.Items.Count - 1
        '
        '    'find or create each node for item
        '
        '    Dim aItem As String = patchesListBox.Items(i).ToString()
        '    GPTrees.AddNode(patchesTreeView.Nodes, aItem, aItem)
        '
        'Next

        'copy each item from listbox
        For Each item In patchesListBox.Items

            'find or create each node for item
            Dim aItem As String = item.ToString()
            GPTrees.AddNode(patchesTreeView.Nodes, aItem, aItem)

        Next


    End Sub

 
    'NB - PAB There is protection against event recursion and then explicit recursion, but could have left it up to event recursion.!!

    ' Updates all child tree nodes recursively. 
    Shared Sub CheckAllChildNodes(treeNode As TreeNode, nodeChecked As Boolean)
        Dim node As TreeNode
        For Each node In treeNode.Nodes
            node.Checked = nodeChecked
            If node.Nodes.Count > 0 Then
                ' If the current node has child nodes, call the CheckAllChildsNodes method recursively. 
                CheckAllChildNodes(node, nodeChecked)
            End If
        Next node
    End Sub

    'Non-recursive
    Shared Sub CheckChildNodes(treeNode As TreeNode, nodeChecked As Boolean)
        Dim node As TreeNode
        For Each node In treeNode.Nodes
            node.Checked = nodeChecked
            If nodeChecked Then
                node.ExpandAll()
            End if
        Next node
    End Sub

 
 

    Shared Sub ReadCheckedNodes(ByRef treeNode As TreeNode, ByRef iChosenPatches As Collection, ByVal itemsOnly As Boolean)

        Try
            Dim node As TreeNode

            If treeNode.Nodes.Count > 0 Then
                For Each node In treeNode.Nodes
                    ReadCheckedNodes(node, iChosenPatches, itemsOnly)
                Next node
            Else
                If treeNode.Checked Or Not itemsOnly Then
                    iChosenPatches.Add(treeNode.FullPath)
                End If

            End If
        Catch ex As System.NullReferenceException
            Logger.Dbg("TreeNode not defined.")
        End Try


    End Sub

 

    Shared Sub RemoveNodes(ByRef givenNodes As TreeNodeCollection, ByVal checked As Boolean)
        Dim node As TreeNode
        For i As Integer = givenNodes.Count - 1 To 0 Step -1

            node = givenNodes(i)

            If node.Nodes.Count > 0 Then
                'non-leaf
                RemoveNodes(node.Nodes, checked)
                'if no leaves left, remove the branch
                If node.Nodes.Count = 0 Then
                    givenNodes.Remove(node)
                End If

            Else
                'leaf
                If node.Checked = checked Then
                    givenNodes.Remove(node)
                End If
            End If
 
        Next

    End Sub
 

End Class
