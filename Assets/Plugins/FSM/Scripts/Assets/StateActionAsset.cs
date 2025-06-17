 using System;

 namespace Modules.FSM
 {
     public interface IStateActionAsset
     {
         Action Create(object context);
     }
}