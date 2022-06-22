import Button, { ButtonProps } from '@mui/material/Button/Button';
import React from 'react';
import { Link, LinkProps } from 'react-router-dom';

interface RouterButtonProps extends Omit<ButtonProps, keyof LinkProps | 'ref'> {
	to: { $: string };
	absolute?: boolean;
	replace?: boolean;
	children?: React.ReactNode;
}

const RouterButton: React.FC<RouterButtonProps> = ({
	to,
	replace,
	children,
	absolute,
	...rest
}) => {
	let link = to.$;
	if ((absolute ?? true) && !link.startsWith('/')) {
		link = `/${link}`;
	}

	return (
		<Button component={Link} to={link} {...rest}>
			{children}
		</Button>
	);
};

export default RouterButton;
