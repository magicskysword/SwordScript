using System.Collections.Generic;

namespace SwordScript;

public abstract class ASTStatement : ASTList
{
    public ASTStatement()
    {
        
    }
    
    public ASTStatement(IEnumerable<ASTNode> children) : base(children)
    {
        
    }
}