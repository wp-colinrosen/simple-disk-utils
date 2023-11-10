mergeInto(LibraryManager.library, {
    GetEstimate: async function (callback) {
        const estimate = await navigator.storage.estimate();

        // Numbers may be too big to fit in a 32-bit integer. Sending 64-bit integers doesn't work properly in Unity,
        // so unfortunately we'll have to convert them to a string.
        let str = estimate.usage + "|" + estimate.quota;
        let strLen = lengthBytesUTF8(str) + 1;
        let ptr = _malloc(strLen);
        stringToUTF8(str, ptr, strLen);

        Module.dynCall_vi(callback, ptr);
    }
});