import { TDSMenuDTO } from "tmt-tang-ui";

export const MenuDto: Array<TDSMenuDTO> = [
    {
        name: 'Quản lý Extension',
        icon: "tdsi-application-fill",
        link: '/dashboard',
    },
    {
        name: 'Lịch sử tác vụ',
        icon: "tdsi-print-fill",
        link: '/history',
    },
    {
        name: 'Máy Ảo',
        icon: "tdsi-dashboard-fill",
        link: '/virtual-machine',
    },
    {
        name: 'Proxy',
        icon: "tdsi-multi-chanel-fill",
        link: '/proxy',
    },
    {
        name: "Tài khoản",
        icon: "tdsi-facebook-2-fill",
        link: '/clientFB',
    },
    {
        name: 'Kịch bản',
        icon: "tdsi-paper-fill",
        link: '/scripts',
    },
    {
        name: "Tài khoản đang sử dụng kịch bản",
        icon: "tdsi-order-fill",
        link: '/proxy-using-script',
    },
    {
        name: "Tài khoản đang hoạt động",
        icon: "tdsi-live-session-fill",
        link: '/active',
    },
    {
        name: "Hồ sơ",
        icon: "tdsi-success-page-line",
        link: '/profile-client?tab=profile',
    },
    {
        name: "Chiến dịch",
        icon: "tdsi-group-product-fill",
        link: '/campaign',
    },
    // {
    //     name: 'Chia sẻ',
    //     icon: "tdsi-share-fill",
    //     link: '/shares',
    // },
    // {
    //     name: 'Cài đặt',
    //     icon: "tdsi-gear-1-fill",
    //     link: '/setting',
    // },
]