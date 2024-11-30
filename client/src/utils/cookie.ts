export function getCookie(name: string) {
    const cookies = document.cookie.split('; ')
    const specificCookie = cookies.find((splitCookie) => splitCookie.startsWith(`${name}=`))
    return specificCookie
}

export function setCookie(name: string, value: string) {
    const cookies = document.cookie.split('; ')
    const specificCookie = cookies.find((splitCookie) => splitCookie.startsWith(`${name}=`))
    const newSpecificCookie = `${name}=${value}`
    const newCookieString = [
        newSpecificCookie,
        ...cookies.filter((cookie) => cookie !== specificCookie)
    ].join('; ')
    document.cookie = newCookieString
}

export function clearCookie(name: string) {
    const cookies = document.cookie.split('; ')
    const specificCookie = cookies.find((splitCookie) => splitCookie.startsWith(`${name}=`))
    const newCookieString = [
        ...cookies.filter((cookie) => cookie !== specificCookie)
    ].join('; ')
    document.cookie = newCookieString
}

export function clearAllCookies() {
    document.cookie = ''
}