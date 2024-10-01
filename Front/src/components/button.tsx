import React from 'react';

interface ButtonProps {
  text: string;                      
  onClick: () => void;               
  disabled?: boolean;                
}

const Button: React.FC<ButtonProps> = ({ text, onClick, disabled }) => {
  return (
    <button 
      className='bg-[#7C3AED] font-["Titillium_Web"] text-white font-bold w-full md:w-[385px] h-[50px] md:h-[50px] rounded-md py-2 px-4'
      onClick={onClick} 
      disabled={disabled}
    >
      {text}
    </button>

  );
};

export default Button;
