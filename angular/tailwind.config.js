
const defaultColors = require('tailwindcss/colors')
//màu nào ko dùng thì comment
const COLORTAIWIND = [
    // "blue",
    // "yellow",
    // "red",
    // "gray",
    // "green",
    // "indigo",
    // "purple",
    // "pink"
]

const COLORS = {
    success: {
        100: '#E9F6EC',
        200: '#DFFCE6',
        300: '#88CE97',
        400: '#28A745',
        500: '#20913A',
    },
    info: {
        100: '#E5F2FF',
        200: '#CCE7FF',
        300: '#72B7FB',
        400: '#2395FF',
        500: '#0184FF',
    },
    warning: {
        100: '#FEF6E9',
        200: '#FCF0CB',
        300: '#F8CE8B',
        400: '#FFC107',
        500: '#F3A72E',
    },
    error: {
        100: '#FDEBE9',
        200: '#FCE6EA',
        300: '#F17585',
        400: '#EB3B5B',
        500: '#DA072D ',
    },
    light: {
        50: '#F8F9FB',
        100: '#F0F1F3',
        200: '#EBEDEF',
    },
    dark: {
        600: "#2B3D4D",
        700: '#1C2B36',
        800: "#212E39",
        900: '#131D26',

    },
    primary: {
        1: 'rgb(45 196 241)',
        2: 'rgb(60 100 175)',
        3: '#2395ff',
    },
    secondary: {
        1: '#3E89FC',
        2: '#70B5FF'
    },
    tertiary: {
        1: '#F59E0B',
        2: '#FCD34D'
    },
    accent: {
        1: '#B5076B',
        2: '#A70000',
        3: '#F33240',
        4: '#FF8900',
        5: '#FFC400',
        6: '#28A745',
        7: '#00875A',
        8: '#0C9AB2',
        9: '#2684FF',
        10: '#034A93',
        11: '#5243AA',
        12: '#42526E'
    },
    gradient: {
        1: '#0051CD',
        2: '#3E89FC'
    },
    'neutral-1': {
        50: "#DDE2E9",
        100: "#CDD3DB",
        200: "#CDD3DB",
        300: "#A1ACB8",
        400: "#929DAA",
        500: "#858F9B",
        600: "#6B7280",
        700: "#5A6271",
        800: "#424752",
        900: "#2C333A",
    },
    'neutral-2': {
        50: "#F2F4F7",
        100: "#E9EDF2",
        200: "#E2E7ED",
        300: "#DFE7EC",
    },
    'neutral-3': {
        50: "#F8F9FB",
        100: "#F0F1F3",
        200: "#EBEDEF",
        300: "#E3E6E9",

    },
    'd-neutral-1': {
        400: "#7D8DA4",
        900: "#C8D4E5",
    },
    'd-neutral-3': {
        600: "#2B3D4D",
        700: "#213240",
        800: "#212E39",
        900: "#131D26",
    }
};
function genarateColorTDS() {
    var colors = [];
    for (const colorName in COLORS) {
        for (const colorOpacity in COLORS[colorName]) {
            colors.push(`${colorName}-${colorOpacity}`)
        }
    }
    if(COLORTAIWIND.length > 0)
    {
        
        for (let index = 0; index < COLORTAIWIND.length; index++) {
            const colorName = COLORTAIWIND[index];
            if(defaultColors[colorName])
            for (const colorOpacity in defaultColors[colorName]) {
                colors.push(`${colorName}-${colorOpacity}`)
            }
        }
    }
    var prefixs = [
        'ring',
        'bg',
        'border',
        'text',
        'focus:bg',
        'focus:border',
        'hover:border',
        'hover:bg',
        'disabled:bg',
        'disabled:border',
        'dark:bg',
        'dark:text',
        'dark:border',
        'dark:group-hover:text',
        'dark:hover:bg',
        'dark:hover:text'
    ]

    var result = [];
    for (let index = 0; index < prefixs.length; index++) {
        const prefix = prefixs[index];
        for (let colorIndex = 0; colorIndex < colors.length; colorIndex++) {
            const color = colors[colorIndex];
            result.push(prefix + "-" + color);
        }
    }
   
    return result;
}
const colorTDS = genarateColorTDS();

const SAFELISTING = [
    // 'ring-opacity-20',
    // 'focus:ring',
    // 'disabled:opacity-65',
    // 'hover:bg-primary-2',
    // 'bg-primary-2',
    // 'focus:bg-primary-2',
    // 'border-primary-2',
    // 'hover:border-primary-2',
    // 'dark:hover:bg-d-neutral-3-700',
    // 'border-b-3',
    // 'border-l-3',
    // 'border-r-3',
    // 'border-t-3',
    // 'border-3',
    // 'h-2',
    // 'w-2',
    // ...colorTDS
]
module.exports = {
    mode: 'jit',
    prefix: '',
    purge: {
        enabled: false,
        content: [
            './src/**/*.{html,ts}',
            './node_modules/tmt-tang-ui/__ivy_ngcc__/fesm2015/tmt-tang-ui.js'           
        ],
        safelist: SAFELISTING

    },
    darkMode: 'class', // or 'media' or 'class'
    theme: {

        extend: {
            zIndex: {
                '60': 60,
                '9999': 9999,
                '1000': 1000
            },
            padding: {
                '1/20': '5%',
              },
            colors: {
                ...COLORS
            },
            ringColor: {
                ...COLORS
            },
            borderColor: {
                ...COLORS,
            },
            boxShadow: {
                'primary': '0px 0px 0px 3px rgba(40, 167, 69, 0.2)',
                'success': '0px 0px 0px 3px rgba(40, 167, 69, 0.2)',
                'error': '0px 0px 0px 3px rgba(235, 59, 91, 0.2)',
                'info': '0px 0px 0px 3px rgba(35, 149, 255, 0.2)',
                'warning': '0px 0px 0px 3px rgba(255, 193, 7, 0.2)',
                '1-lg': '0px 1px 10px rgba(29, 45, 73, 0.102)',
                '1-sm': '0px 1px 3px rgba(29, 45, 73, 0.102)',
                '1-md': '0px 1px 3px rgba(29, 45, 73, 0.102)'
            },
            minWidth: {
                '5': '1.25rem',
                '7': '1.75rem',
                '32': '8rem',
                20: '5rem',
                100: '100px',
                170: '170px',
            },
            minHeight: {
                24: "24px",
                '7': '1.75rem',
            },
            opacity: {
                '65': '.65'
            },
            fontSize: {
                'body-1': ['16px', '24px'],
                'body-2': ['14px', '20px'],
                'title-1': ['16px', '24px'],
                'header-1': ['20px', '28px'],
                'header-2': ['18px', '28px'],
                'caption-1': ['13px', '20px'],
                'caption-2': ['12px', '16px'],
            },
            placeholderColor: {
                ...COLORS
            },
            ringWidth: {
                '3': '3px'
            },
            width: {
                '3/10': '30%',
                '4/10': '40%',
                '5/10': '60%',
                '9/10': '90%',
                '6/10': '65%',
            },
            height: {
                sm: '30px',
                md: '34px',
                lg: '38px',
                '4vh': '4vh'
            },
            borderRadius: {
                '10': "0.625rem"
            },
            fontWeight: {
                regular: 400
            },
            borderWidth: {
                3: "3px"
            }
        },
    },
    variants: {
        extend: {
            opacity: ['disabled'],
            cursor: ['disabled'],
            backgroundColor: ['checked', 'responsive', 'hover', 'focus', 'active', 'disabled'],
            borderColor: ['checked', 'disabled'],
            borderWidth: ['last', 'first'],
            ringWidth: ['hover', 'group-hover'],
            ringColor: ['hover', 'group-hover'],
            ringOpacity: ['hover', 'group-hover'],
            ringOffsetColor: ['hover', 'group-hover'],
            ringOffsetWidth: ['hover', 'group-hover'],
            borderRadius: ['group-hover', 'first', 'last'],
            display: ['group-hover'],
            textColor: ['responsive', 'hover', 'focus', 'active', 'disabled'],
            overflow: ['hover', 'focus'],
            fontWeight: ['dark']
        },
    },
    plugins: [require('@tailwindcss/forms'), require('@tailwindcss/typography')],
};

        
