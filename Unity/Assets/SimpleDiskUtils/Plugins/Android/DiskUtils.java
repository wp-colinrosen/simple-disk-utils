package com.dikra.diskutils;

/**
 * Created by dikra-prasetya on 4/1/16.
 */

import android.os.Environment;
import android.os.StatFs;

import java.math.BigInteger;


public final class DiskUtils {
    private static final long MEGA_BYTE = 1048576;


    /**
     * Calculates total space on disk.
     *
     * @param external Queries external disk if true, queries internal disk otherwise.
     * @return Total disk space in MB.
     */
    public static int totalSpace(boolean external) {
        var statFs = getStats(external);
        var totalBlocks = statFs.getBlockCountLong();
        var blockSize = statFs.getBlockSizeLong();
        var total = BigInteger.valueOf(totalBlocks).multiply(BigInteger.valueOf(blockSize)).divide(BigInteger.valueOf(MEGA_BYTE));
        return total.intValue();
    }

    /**
     * Calculates available space on disk.
     *
     * @param path Gets the disk that contains the path, queries the internal disk if this is null or empty
     * @return Available disk space in MB.
     */
    public static int availableSpace(String path) {
        if (path == null || path.isEmpty()) {
            path = Environment.getRootDirectory().getAbsolutePath();
        }

        var statFs = new StatFs(path);
        var totalBlocks = statFs.getAvailableBlocksLong();
        var blockSize = statFs.getBlockSizeLong();
        var total = BigInteger.valueOf(totalBlocks).multiply(BigInteger.valueOf(blockSize)).divide(BigInteger.valueOf(MEGA_BYTE));
        return total.intValue();
    }

    /**
     * Calculates available space on disk.
     *
     * @param external Queries external disk if true, queries internal disk otherwise.
     * @return Available disk space in MB.
     */
    public static int availableSpace(boolean external) {
        var statFs = getStats(external);
        var availableBlocks = statFs.getAvailableBlocksLong();
        var blockSize = statFs.getBlockSizeLong();
        var free = BigInteger.valueOf(availableBlocks).multiply(BigInteger.valueOf(blockSize)).divide(BigInteger.valueOf(MEGA_BYTE));
        return free.intValue();
    }

    /**
     * Calculates busy space on disk.
     *
     * @param external Queries external disk if true, queries internal disk otherwise.
     * @return Busy disk space in MB.
     */
    public static int busySpace(boolean external) {
        var statFs = getStats(external);
        var total = BigInteger.valueOf(statFs.getBlockCountLong()).multiply(BigInteger.valueOf(statFs.getBlockSizeLong()));
        var free = BigInteger.valueOf(statFs.getFreeBlocksLong()).multiply(BigInteger.valueOf(statFs.getBlockSizeLong()));
        var ret = total.subtract(free).divide(BigInteger.valueOf(MEGA_BYTE));
        return ret.intValue();
    }


    private static StatFs getStats(boolean external) {
        String path;

        if (external) {
            path = Environment.getExternalStorageDirectory().getAbsolutePath();
        } else {
            path = Environment.getRootDirectory().getAbsolutePath();
        }

        return new StatFs(path);
    }
}