import Link, { LinkProps } from '@mui/material/Link/Link';
import React from 'react';
import { Link as ReactRouterDomLink } from 'react-router-dom';

interface RouterLinkProps extends LinkProps {
	to: { $: string };
	replace?: boolean;
	absolute?: boolean;
}

const RouterLink: React.FC<RouterLinkProps> = ({ to, absolute, ...rest }) => {
	let link = to.$;
	if ((absolute ?? true) && !link.startsWith('/')) {
		link = `/${link}`;
	}

	return (
		<Link
			underline='none'
			to={link}
			{...rest}
			component={ReactRouterDomLink as any}
		/>
	);
};

export default RouterLink;
