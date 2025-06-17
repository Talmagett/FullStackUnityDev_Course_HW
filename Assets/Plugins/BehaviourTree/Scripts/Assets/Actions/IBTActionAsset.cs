 using System;

 namespace Modules.BehaviourTree
 {
     public interface IBTActionAsset
     {
         Action Create(object context);
     }
 }