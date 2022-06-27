import languages from '@cospired/i18n-iso-languages';
import countries from 'i18n-iso-countries';
import React from 'react';
import { createRoot } from 'react-dom/client';
import './presentation/i18n';
import { Root } from './presentation/Root';

countries.registerLocale(require('i18n-iso-countries/langs/de.json'));
countries.registerLocale(require('i18n-iso-countries/langs/en.json'));

languages.registerLocale(require('@cospired/i18n-iso-languages/langs/en.json'));
languages.registerLocale(require('@cospired/i18n-iso-languages/langs/de.json'));

const rootElement = document.getElementById('root');
const root = createRoot(rootElement!);

root.render(<Root />);
