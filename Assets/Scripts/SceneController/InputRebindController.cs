using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneController
{
    
    /// <summary>
    /// Author: Alexander Wyss
    /// Allows the inputs to be configured. And sets the ui elements.
    /// </summary>
    public class InputRebindController: MonoBehaviour
    {
        public InputActionAsset actions;
        private InputActionMap actionMap;
        
        private InputAction moveInput;
        public TextMeshProUGUI forwardText;
        private int forwardIndex = 1;
        public TextMeshProUGUI backText;
        private int backIndex = 2;
        public TextMeshProUGUI leftText;
        private int leftIndex = 3;
        public TextMeshProUGUI rightText;
        private int rightIndex = 4;
        
        private InputAction jump;
        public TextMeshProUGUI jumpText;
        private int jumpIndex = 0;
        private InputAction fire;
        public TextMeshProUGUI fireText;
        private int fireIndex = 0;
       

        /// <summary>
        /// Initialize all action maps and ui elements.
        /// </summary>
        public void Awake()
        {
            actionMap = actions.FindActionMap("input");
            moveInput = actionMap.FindAction("move");
            forwardText.text = moveInput.GetBindingDisplayString(forwardIndex);
            backText.text = moveInput.GetBindingDisplayString(backIndex);
            leftText.text = moveInput.GetBindingDisplayString(leftIndex);
            rightText.text = moveInput.GetBindingDisplayString(rightIndex);
            
            jump = actionMap.FindAction("jump");
            jumpText.text = jump.GetBindingDisplayString(jumpIndex);
            
            fire = actionMap.FindAction("fire");
            fireText.text = fire.GetBindingDisplayString(fireIndex);
        }

        public void RebindForward()
        {
            RemapButtonClicked(moveInput, forwardIndex, forwardText);
        }
        
        public void RebindBack()
        {
            RemapButtonClicked(moveInput, backIndex, backText);
        }

        public void RebindLeft()
        {
            RemapButtonClicked(moveInput, leftIndex, leftText);
        }
        
        public void RebindRight()
        {
            RemapButtonClicked(moveInput, rightIndex, rightText);
        }
        
        public void RebindJump()
        {
            RemapButtonClicked(jump, jumpIndex, jumpText);
        }
        
        public void RebindFire()
        {
            RemapButtonClicked(fire, fireIndex, fireText);
        }
        
        /// <summary>
        /// Waits for physical input of the player and remaps the given action to the input.
        /// Sets the new text on the provided ui element.
        /// </summary>
        /// <param name="actionToRebind"></param>
        /// <param name="bindingIndex"></param>
        /// <param name="text"></param>
        void RemapButtonClicked(InputAction actionToRebind, int bindingIndex, TextMeshProUGUI text)
        {
            text.text = "<->";
            actionToRebind.PerformInteractiveRebinding()
                .WithTargetBinding(bindingIndex)
                .OnMatchWaitForAnother(0.1f)
                .Start()
                .OnComplete(rebind => text.text = rebind.action.GetBindingDisplayString(bindingIndex))
                .OnCancel(rebind => text.text = actionToRebind.GetBindingDisplayString(bindingIndex));
        }
    }
}