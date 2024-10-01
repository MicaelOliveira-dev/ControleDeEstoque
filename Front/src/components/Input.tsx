import React, { useRef } from 'react';

interface InputProps {
    name: string;
    text: string;
    type: string;
    placeholder?: string;       
    value?: string;  
    onChange: (e: React.ChangeEvent<HTMLInputElement>) => void; 
    onIconClick: () => void; 
    icon?: React.ReactNode;
}

const Input: React.FC<InputProps> = ({
    name,
    text,
    type,
    placeholder = '',
    value = '',
    onChange,
    onIconClick, 
    icon 
}) => {
  const inputRef = useRef<HTMLInputElement | null>(null);


  return (
    <div>
        <label htmlFor={name} className="block text-sm font-semibold font-['Titillium_Web'] mb-[8px]">
            {text}
        </label>
        <div 
            className="flex mt-1 block w-full md:w-[385px] h-[50px] md:h-[50px] border border-[#E2E8F0] rounded-md shadow-sm justify-between"
        > 
            <input
                ref={inputRef} 
                id={name}
                type={type}
                placeholder={placeholder}
                value={value}
                onChange={onChange}
                className={`w-[90%] font-['Titillium_Web'] text-black outline-none pr-10 pl-[12px]`} 
                style={{ paddingRight: '2.5rem' }} 
            />
            {icon && (
                <span 
                    onClick={onIconClick} 
                    className="flex items-center cursor-pointer mr-[12px] text-gray-400"
                >
                    {icon} 
                </span>
            )}
        </div>
    </div>
  );
};

export default Input;
