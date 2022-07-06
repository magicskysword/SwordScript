using System.Collections;
using System.Collections.Generic;

namespace SwordScript;

public abstract class ASTList : ASTBase, IEnumerable<ASTBase>
{
    public ASTList()
    {
        
    }
    
    public ASTList(IEnumerable<ASTBase> list)
    {
        _children.AddRange(list);
    }
    
    private List<ASTBase> _children = new List<ASTBase>();
    public IReadOnlyList<ASTBase> Children => _children;
    public ASTBase this[int i] => _children[i];
    public IEnumerator<ASTBase> GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}